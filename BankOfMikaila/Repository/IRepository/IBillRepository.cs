using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IBillRepository : IRepository<Bill>
    {
        void Update(Bill bill);
    }
}
