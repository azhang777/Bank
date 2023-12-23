using BankOfMikaila.Models;

namespace BankOfMikaila.Repository.IRepository
{
    public interface IP2PRepository : IRepository<P2P>
    {
        void Update(P2P p2p);
    }
}
