using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class DepositRepository : Repository<Deposit>, IDepositRepository
    {
        private readonly ApplicationDbContext _db;

        public DepositRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Deposit deposit)
        {
            _db.Update(deposit);
            _db.SaveChanges();
        }
    }
}
