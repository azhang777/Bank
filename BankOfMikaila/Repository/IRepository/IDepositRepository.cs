using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IDepositRepository : IRepository<Deposit>
    {
        void Update(Deposit deposit);
    }
}
