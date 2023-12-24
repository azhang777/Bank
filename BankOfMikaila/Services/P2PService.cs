using BankOfMikaila.Models;
using BankOfMikaila.Repository;

namespace BankOfMikaila.Services
{
    public class P2PService
    {
        private readonly P2PRepository _p2pRepository;
        private readonly AccountRepository _accountRepository;

        public P2PService(P2PRepository p2pRepository, AccountRepository accountRepository)
        {
            _p2pRepository = p2pRepository;
            _accountRepository = accountRepository;
        }

        public P2P CreateP2P(long payerId, P2P p2p)
        {
            var payerAccount = _accountRepository.Get(payerId);
            var payeeAccount = _accountRepository.Get(p2p.ReceiverId);

            payerAccount.Balance -= p2p.Amount;
            payeeAccount.Balance += p2p.Amount;

            _p2pRepository.Create(p2p);
            _p2pRepository.Save();
            _accountRepository.Save();

            return p2p;
        }

        public P2P GetP2P(long p2pId)
        {
            return _p2pRepository.Get(p2pId);
        }
    }
}
