using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Update(Customer customer);
    }
}
