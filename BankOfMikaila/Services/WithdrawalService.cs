using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using BankOfMikaila.Repository;
using BankOfMikaila.Repository.IRepository;
using Hangfire;
using Microsoft.Identity.Client;

namespace BankOfMikaila.Services
{
    public class WithdrawalService
    {
        public readonly IWithdrawalRepository _withdrawalRepository;
        public readonly IAccountRepository _accountRepository;

        public WithdrawalService(IWithdrawalRepository withdrawalRepository, IAccountRepository accountRepository)
        {
            _withdrawalRepository = withdrawalRepository;
            _accountRepository = accountRepository;
        }

        public Withdrawal CreateWithdrawal(long accountId, Withdrawal withdrawal)
        {
            VerifyWithdrawal(withdrawal);
            //check if account exists
            var account = _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found");
            withdrawal.Account = account;
            withdrawal.AccountId = accountId;
            //add withdrawal to the transactions database.
            _withdrawalRepository.Create(withdrawal);
            _withdrawalRepository.Save();
            //_accountRepository.Save();
            BackgroundJob.Schedule(() => CompleteWithdrawal(withdrawal.Id), TimeSpan.FromSeconds(6)); //CompleteWithdrawal checks if withdrawal is pending and if account has enough to withdrawal
            
            return withdrawal;
        }

        public Withdrawal GetWithdrawal(long withdrawalId)
        {
            return _withdrawalRepository.Get(withdrawalId, withdrawal => withdrawal.Account) ?? throw new TransactionNotFoundException("Withdrawal " + withdrawalId + " not found");
        }

        public IEnumerable<Withdrawal> GetWithdrawalsByAccount(long accountId)
        {
            var withdrawals = _withdrawalRepository.GetAllFiltered(withdrawal => withdrawal.AccountId == accountId);

            if (withdrawals.Count == 0)
            {
                throw new TransactionNotFoundException("No withdrawals found");
            }

            return withdrawals;
        }

        public Withdrawal UpdateWithdrawal(long withdrawalId, Withdrawal updatedWithdrawal)
        {
            VerifyWithdrawal(updatedWithdrawal);
            
            var existingWithdrawal = GetWithdrawal(withdrawalId);
            var originalAccount = _accountRepository.Get(existingWithdrawal.AccountId);

            if (originalAccount.Balance + existingWithdrawal.Amount < updatedWithdrawal.Amount)
            {
                throw new NoFundsAvailableException("Account " + originalAccount.Id + " does not have available funds to make this updated transaction");
            }

            var originalAmount = existingWithdrawal.Amount;

            existingWithdrawal.TransactionType = updatedWithdrawal.TransactionType;
            existingWithdrawal.TransactionDate = updatedWithdrawal.TransactionDate;
            existingWithdrawal.TransactionStatus = updatedWithdrawal.TransactionStatus;
            existingWithdrawal.Amount = updatedWithdrawal.Amount;
            existingWithdrawal.Description = updatedWithdrawal.Description;

            originalAccount.Balance -= (updatedWithdrawal.Amount - originalAmount); //throw an exception if transaction error occurs? Withdrawal cannot be done if balance < withdrawal amount

            _withdrawalRepository.Update(existingWithdrawal);
            _withdrawalRepository.Save();
            _accountRepository.Save();

            return updatedWithdrawal;
        }

        //can not delete a deposit, but we can cancel a deposit...

        public void CancelWithdrawal(long withdrawalId)
        {
            var existingWithdrawal = GetWithdrawal(withdrawalId);
            VerifyWithdrawal(existingWithdrawal);
            var originalAccount = _accountRepository.Get(existingWithdrawal.AccountId);

            existingWithdrawal.TransactionStatus = TransactionStatus.CANCELED;
            originalAccount.Balance += existingWithdrawal.Amount;

            _withdrawalRepository.Save();
            _accountRepository.Save();
        }

        private static void VerifyWithdrawal(Withdrawal withdrawal)
        {
            if (withdrawal.TransactionType != TransactionType.WITHDRAWAL)
            {
                throw new InvalidTransactionTypeException("Withdrawal type is invalid");
            }
            else if (withdrawal.TransactionStatus != TransactionStatus.PENDING && withdrawal.TransactionStatus != TransactionStatus.RECURRING)
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to modify withdrawal " + withdrawal.Id);
            }
        }

        public void CompleteWithdrawal(long withdrawalId)
        {

            var withdrawal = GetWithdrawal(withdrawalId);

            if (withdrawal.TransactionStatus != TransactionStatus.PENDING) //recurring implementation will not be done.
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to complete withdrawal " + withdrawal.Id);
            }

            var account = withdrawal.Account;

            if (account.Balance < withdrawal.Amount)
            {
                throw new NoFundsAvailableException("Account " + account.Id + " does not have available funds to make this transaction");
            }


            account.Balance -= withdrawal.Amount;
            withdrawal.TransactionStatus = TransactionStatus.COMPLETED;

            _withdrawalRepository.Save();
            _accountRepository.Save();
        }
    }
}
