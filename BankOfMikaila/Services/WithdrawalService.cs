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
            withdrawal.Account1 = account;
            withdrawal.Account1_Id = accountId;
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
    }
}
