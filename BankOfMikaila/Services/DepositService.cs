using BankOfMikaila.Models;
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
            //check if account exists
            var account = _accountRepository.Get(accountId);
            //add account balance with deposit amount
            deposit.Account1 = account;
            deposit.Account1_Id = accountId;
            account.Balance += deposit.Amount;
            //add deposit to the transactions database.
            _depositRepository.Create(deposit);
            _depositRepository.Save();
            _accountRepository.Save();

            return deposit;
        }

        public Deposit GetDeposit(long depositId)
        {
            return _depositRepository.Get(depositId);
        }

        public IEnumerable<Deposit> GetDepositsByAccount(long accountId)
        {
            return _depositRepository.GetAllFiltered(deposit => deposit.Account1_Id == accountId);
        }

        public Deposit UpdateDeposit(long depositId, Deposit updatedDeposit)
        {
            var existingDeposit = GetDeposit(depositId);
            var originalAccount = _accountRepository.Get(existingDeposit.Account1_Id);
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

        public void cancelDeposit(long depositId)
        {
            var existingDeposit = GetDeposit(depositId);
            var originalAccount = _accountRepository.Get(existingDeposit.Account1_Id);

            //if only in pending state
            existingDeposit.TransactionStatus = Models.Enum.TransactionStatus.CANCELED;
            originalAccount.Balance -= existingDeposit.Amount;
        }
        
    }
}
