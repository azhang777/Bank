using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using BankOfMikaila.Repository.IRepository;

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
            
            if (account.Balance < withdrawal.Amount)
            {
                throw new NoFundsAvailableException("Account " + accountId + " does not have available funds to make this transaction");
            }
            //add account balance with withdrawal amount
            withdrawal.Account = account;
            withdrawal.AccountId = accountId;
            account.Balance -= withdrawal.Amount;
            //add withdrawal to the transactions database.
            _withdrawalRepository.Create(withdrawal);
            _withdrawalRepository.Save();
            _accountRepository.Save();

            return withdrawal;
        }

        public Withdrawal GetWithdrawal(long withdrawalId)
        {
            return _withdrawalRepository.Get(withdrawalId) ?? throw new TransactionNotFoundException("Withdrawal " + withdrawalId + " not found");
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
            var originalAccount = _accountRepository.Get(existingWithdrawal.AccountId);

            //if only in pending state
            if (existingWithdrawal.TransactionStatus == TransactionStatus.PENDING)
            {
                existingWithdrawal.TransactionStatus = TransactionStatus.CANCELED;
                originalAccount.Balance += existingWithdrawal.Amount;
            }
            else
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to cancel withdrawal " + withdrawalId);
            }

            _withdrawalRepository.Save();
            _accountRepository.Save();
        }

        private static void VerifyWithdrawal(Withdrawal withdrawal)
        {
            if (withdrawal.TransactionType != TransactionType.WITHDRAWAL)
            {
                throw new InvalidTransactionTypeException("Withdrawal type is invalid");
            }
        }
    }
}
