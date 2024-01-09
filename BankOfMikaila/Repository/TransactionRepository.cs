using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public TransactionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Transaction transaction)
        {
            _db.Update(transaction);
            _db.SaveChanges();
        }
    }
}
