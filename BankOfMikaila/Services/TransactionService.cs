using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public Transaction CreateWithdrawal(long accountId, Transaction withdrawal)
        {
            //check if account exists
            var account = _accountRepository.Get(accountId);
            //subtract account balance with withdrawal amount
            withdrawal.Account1 = account;
            withdrawal.Account1_Id = accountId;
            account.Balance -= withdrawal.Amount;
            //add withdrawal to the transactions database.
            _transactionRepository.Create(withdrawal);
            _transactionRepository.Save();
            _accountRepository.Save();

            return withdrawal;
        }

        public Transaction CreateDeposit(long accountId, Transaction deposit)
        {
            //check if account exists
            var account = _accountRepository.Get(accountId);
            //add account balance with deposit amount
            deposit.Account1 = account;
            deposit.Account1_Id = accountId;
            account.Balance += deposit.Amount;
            //add deposit to the transactions database.
            _transactionRepository.Create(deposit);
            _transactionRepository.Save();
            _accountRepository.Save();

            return deposit;
        }
    }
}
