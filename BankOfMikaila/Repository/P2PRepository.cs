using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class P2PRepository : Repository<P2P>, IP2PRepository
    {
        private readonly ApplicationDbContext _db;

        public P2PRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(P2P p2p)
        {
            _db.Update(p2p);
            _db.SaveChanges();
        }
    }
}
