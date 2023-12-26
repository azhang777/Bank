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
            VerifyP2P(p2p);

            var payerAccount = _accountRepository.Get(payerId) ?? throw new AccountNotFoundException("Account " + payerId + " not found");

            if (payerAccount.Balance < p2p.Amount)
            {
                throw new NoFundsAvailableException("Account " + payerId + " does not have available funds to make this transaction");
            }

            var payeeAccount = _accountRepository.Get(p2p.ReceiverId) ?? throw new AccountNotFoundException("Account " + p2p.ReceiverId + " not found");

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

        private static void VerifyP2P(P2P p2p)
        {
            if (p2p.TransactionType != Models.Enum.TransactionType.P2P)
            {
                throw new InvalidTransactionTypeException("P2P type is invalid");
            }
        }
    }
}
