using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IWithdrawalRepository : IRepository<Withdrawal>
    {
        void Update(Withdrawal withdrawal);
    }
}
