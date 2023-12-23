using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        void Update(Transaction transaction);
    }
}
