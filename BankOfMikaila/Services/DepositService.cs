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
    }
}
