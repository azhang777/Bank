using BankOfMikaila.Data;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Customer customer)
        {
            _db.Update(customer);
            _db.SaveChanges();
        }
    }
}
