using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext _db;

        public BillRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
        
        public void Update(Bill bill)
        {
            _db.Update(bill);
            _db.SaveChanges();
        }


    }
}
