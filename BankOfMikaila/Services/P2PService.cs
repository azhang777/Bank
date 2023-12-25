using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Services
{
    public class P2PService
    {
        private readonly IP2PRepository _p2pRepository;
        private readonly IAccountRepository _accountRepository;

        public P2PService(IP2PRepository p2pRepository, IAccountRepository accountRepository)
        {
            _p2pRepository = p2pRepository;
            _accountRepository = accountRepository;
        }

        public P2P CreateP2P(long payerId, P2P p2p)
        {
            var payerAccount = _accountRepository.Get(payerId) ?? throw new AccountNotFoundException("Account " + payerId + " not found");
            var payeeAccount = _accountRepository.Get(p2p.ReceiverId);
            p2p.AccountId = payerId;
            payerAccount.Balance -= p2p.Amount;
            payeeAccount.Balance += p2p.Amount;

            _p2pRepository.Create(p2p);
            _p2pRepository.Save();
            _accountRepository.Save();

            return p2p;
        }

        public P2P GetP2P(long p2pId)
        {
            return _p2pRepository.Get(p2pId) ?? throw new TransactionNotFoundException("P2P of id " + p2pId + " not found");
        }
    }
}
