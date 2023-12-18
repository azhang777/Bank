using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IAccountRepository : IRepository<Account>
    {
        void Update(Account account);
    }
}
