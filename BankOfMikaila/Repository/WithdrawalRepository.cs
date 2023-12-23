using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class WithdrawalRepository : Repository<Withdrawal>, IWithdrawalRepository
    {
        private readonly ApplicationDbContext _db;

        public WithdrawalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Withdrawal withdrawal)
        {
            _db.Update(withdrawal);
            _db.SaveChanges();
        }
    }
}
