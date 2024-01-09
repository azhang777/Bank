using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
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
