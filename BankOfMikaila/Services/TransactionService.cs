using BankOfMikaila.Exceptions;
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

        public IEnumerable<Transaction> GetAllTransactions()
        {
            var transactions = _transactionRepository.GetAll();

            if (transactions.Count == 0)
            {
                throw new TransactionNotFoundException("No transactions found");
            }

            return transactions;
        }

    }
}
