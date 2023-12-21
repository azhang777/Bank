using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IAddressRepository: IRepository<Address>
    {
        void Update(Address address);
    }
}
