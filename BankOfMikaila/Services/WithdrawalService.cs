using BankOfMikaila.Models;
using BankOfMikaila.Repository;
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
            //check if account exists
            var account = _accountRepository.Get(accountId);
            //add account balance with withdrawal amount
            withdrawal.Owner = account;
            withdrawal.OwnerId = accountId;
            account.Balance -= withdrawal.Amount;
            //add withdrawal to the transactions database.
            _withdrawalRepository.Create(withdrawal);
            _withdrawalRepository.Save();
            _accountRepository.Save();

            return withdrawal;
        }

        public Withdrawal GetWithdrawal(long withdrawalId)
        {
            return _withdrawalRepository.Get(withdrawalId);
        }

        public IEnumerable<Withdrawal> GetWithdrawalsByAccount(long accountId)
        {
            return _withdrawalRepository.GetAllFiltered(withdrawal => withdrawal.OwnerId == accountId); ;
        }

        public Withdrawal UpdateWithdrawal(long withdrawalId, Withdrawal updatedWithdrawal)
        {
            var existingWithdrawal = GetWithdrawal(withdrawalId);
            var originalAccount = _accountRepository.Get(existingWithdrawal.OwnerId);
            var originalAmount = existingWithdrawal.Amount;

            existingWithdrawal.TransactionType = updatedWithdrawal.TransactionType;
            existingWithdrawal.TransactionDate = updatedWithdrawal.TransactionDate;
            existingWithdrawal.TransactionStatus = updatedWithdrawal.TransactionStatus;
            existingWithdrawal.Amount = updatedWithdrawal.Amount;
            existingWithdrawal.Description = updatedWithdrawal.Description;

            originalAccount.Balance -= (updatedWithdrawal.Amount - originalAmount);

            _withdrawalRepository.Update(existingWithdrawal);
            _withdrawalRepository.Save();
            _accountRepository.Save();

            return updatedWithdrawal;
        }

        //can not delete a deposit, but we can cancel a deposit...

        public void CancelWithdrawal(long withdrawalId)
        {
            var existingWithdrawal = GetWithdrawal(withdrawalId);
            var originalAccount = _accountRepository.Get(existingWithdrawal.OwnerId);

            //if only in pending state
            existingWithdrawal.TransactionStatus = Models.Enum.TransactionStatus.CANCELED;
            originalAccount.Balance += existingWithdrawal.Amount;

            _withdrawalRepository.Save();
            _accountRepository.Save();
        }
    }
}
