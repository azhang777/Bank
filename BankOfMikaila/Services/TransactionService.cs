using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;
using System.Collections;

namespace BankOfMikaila.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICacheService _cacheService;

        public TransactionService(ITransactionRepository transactionRepository, ICacheService cacheService)
        {
            _transactionRepository = transactionRepository;
            _cacheService = cacheService;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            
            var cacheData = _cacheService.GetData<List<Transaction>>("transactions");

            if (cacheData != null)
            {
                return cacheData;
            }
            else
            {
                cacheData = _transactionRepository.GetAll();

                var expiryTime = DateTimeOffset.Now.AddSeconds(40);

                _cacheService.SetData("transactions", cacheData, expiryTime);
            }

            var transactions = _transactionRepository.GetAll();

            if (transactions.Count == 0)
            {
                throw new TransactionNotFoundException("No transactions found");
            }

            return transactions;
        }
    }
}
