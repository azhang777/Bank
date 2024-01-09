using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using BankOfMikaila.Repository.IRepository;
using Hangfire;

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
            deposit.Account = account;
            deposit.AccountId = accountId;
            //add deposit to the transactions database.
            _depositRepository.Create(deposit);
            _depositRepository.Save();
            //_accountRepository.Save(); //will be saved in the CompleteDeposit method
            BackgroundJob.Schedule(() => CompleteDeposit(deposit.Id), TimeSpan.FromSeconds(5)); //seems that hangfire serializes job parameters. Need to pass in simple id to get deposit within the method rather than pass in complex object Deposit itself. The method will fail because if we pass a deposit object.
            
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
            VerifyDeposit(existingDeposit);
            var originalAccount = _accountRepository.Get(existingDeposit.AccountId); //if deposit exist, account must exist. no need to throw exception here

            existingDeposit.TransactionStatus = TransactionStatus.CANCELED;
            originalAccount.Balance -= existingDeposit.Amount;

            _depositRepository.Save(); //forgot to changes to database... thats why the endpoint did not work
            _accountRepository.Save();
        }

        private static void VerifyDeposit(Deposit deposit)
        {
            if (deposit.TransactionType != TransactionType.DEPOSIT)
            {
                throw new InvalidTransactionTypeException("Deposit type is invalid");
            }
            else if (deposit.TransactionStatus != TransactionStatus.PENDING || deposit.TransactionStatus != TransactionStatus.RECURRING)
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to modify deposit " + deposit.Id);
            }
        }

        public void CompleteDeposit(long depositId)
        {

            var deposit = GetDeposit(depositId);

            if (deposit.TransactionStatus != TransactionStatus.PENDING)
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to complete deposit "  + deposit.Id);
            }

            var account = deposit.Account;
            account.Balance += deposit.Amount;
            deposit.TransactionStatus = TransactionStatus.COMPLETED;

            _depositRepository.Save();
            _accountRepository.Save();
        }
    }
}
