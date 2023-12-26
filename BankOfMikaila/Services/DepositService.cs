using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Services
{
    public class DepositService
    {
        public readonly IDepositRepository _depositRepository;
        public readonly IAccountRepository _accountRepository;

        public DepositService(IDepositRepository depositRepository, IAccountRepository accountRepository)
        {
            _depositRepository = depositRepository;
            _accountRepository = accountRepository;
        }

        public Deposit CreateDeposit(long accountId, Deposit deposit)
        {
            VerifyDeposit(deposit);
            //check if account exists
            var account = _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found");
            //add account balance with deposit amount
            deposit.Account = account;
            deposit.AccountId = accountId;
            account.Balance += deposit.Amount;
            //add deposit to the transactions database.
            _depositRepository.Create(deposit);
            _depositRepository.Save();
            _accountRepository.Save();

            return deposit;
        }

        public Deposit GetDeposit(long depositId)
        {
            return _depositRepository.Get(depositId) ?? throw new TransactionNotFoundException("Deposit " + depositId + " not found" );
        }

        public IEnumerable<Deposit> GetDepositsByAccount(long accountId)
        {
            var deposits = _depositRepository.GetAllFiltered(deposit => deposit.AccountId == accountId);

            if (deposits.Count == 0)
            {
                throw new TransactionNotFoundException("No deposits found"); //MUST CHECK THIS. IF THERE IS WITHDRAWALS AMD P2P, DOES IT AFFECT THE SIZE OF DEPOSIT?
            }

            return deposits;
        }

        public Deposit UpdateDeposit(long depositId, Deposit updatedDeposit)
        {
            VerifyDeposit(updatedDeposit);

            var existingDeposit = GetDeposit(depositId);
            var originalAccount = _accountRepository.Get(existingDeposit.AccountId); /*?? throw new AccountNotFoundException("Account to update is not found"); this is not needed bc if bc deposit exists only if account exists. If deposit exists, the account must exist. */
            var originalAmount = existingDeposit.Amount;

            existingDeposit.TransactionType = updatedDeposit.TransactionType;
            existingDeposit.TransactionDate = updatedDeposit.TransactionDate;
            existingDeposit.TransactionStatus = updatedDeposit.TransactionStatus;
            existingDeposit.Amount = updatedDeposit.Amount;
            existingDeposit.Description = updatedDeposit.Description;

            originalAccount.Balance += (updatedDeposit.Amount - originalAmount); 

            _depositRepository.Update(existingDeposit);
            _depositRepository.Save();
            _accountRepository.Save();

            return updatedDeposit;
        }

        //can not delete a deposit, but we can cancel a deposit...

        public void CancelDeposit(long depositId) //may change to bool
        {
            var existingDeposit = GetDeposit(depositId);
            var originalAccount = _accountRepository.Get(existingDeposit.AccountId); //if deposit exist, account must exist. no need to throw exception here

            //if only in pending state
            if (existingDeposit.TransactionStatus == TransactionStatus.PENDING)
            {
                existingDeposit.TransactionStatus = TransactionStatus.CANCELED;
                originalAccount.Balance -= existingDeposit.Amount;
            }
            else
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to cancel deposit " + depositId);
            }

            _depositRepository.Save(); //forgot to changes to database... thats why the endpoint did not work
            _accountRepository.Save();
        }

        private static void VerifyDeposit(Deposit deposit)
        {
            if (deposit.TransactionType != TransactionType.DEPOSIT)
            {
                throw new InvalidTransactionTypeException("Deposit type is invalid");
            }
        }

    }
}
