using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Models.Enum;
using BankOfMikaila.Repository.IRepository;
using Hangfire;

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
            p2p.Account = payerAccount;
            p2p.Receiver = payeeAccount; //from P2PCreateDTO, only receiverId is linked. We need to link the account, account id, and receiver to the p2p object

            _p2pRepository.Create(p2p);
            _p2pRepository.Save();
            //_accountRepository.Save();
            BackgroundJob.Schedule(() => CompleteP2P(p2p.Id), TimeSpan.FromSeconds(6)); //p2p id is created by line 37

            return p2p;
        }

        public P2P GetP2P(long p2pId)
        {
            return _p2pRepository.Get(p2pId, p2p => p2p.Account, p2p => p2p.Receiver) ?? throw new TransactionNotFoundException("P2P of id " + p2pId + " not found");
        }

        private static void VerifyP2P(P2P p2p)
        {
            if (p2p.TransactionType != Models.Enum.TransactionType.P2P)
            {
                throw new InvalidTransactionTypeException("P2P type is invalid");
            }
            else if (p2p.TransactionStatus != TransactionStatus.PENDING && p2p.TransactionStatus != TransactionStatus.RECURRING) //is not really needed as we do not have update or cancel p2p
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to modify p2p " + p2p.Id);
            }
        }

        public void CompleteP2P(long p2pId)
        {
            var p2p = GetP2P(p2pId);

            if (p2p.TransactionStatus != TransactionStatus.PENDING)
            {
                throw new InvalidTransactionStatusException("Invalid status: unable to complete p2p " + p2p.Id);
            }

            var payerAccount = p2p.Account;
            var payeeAccount = p2p.Receiver;

            payerAccount.Balance -= p2p.Amount;
            payeeAccount.Balance += p2p.Amount;
            p2p.TransactionStatus = TransactionStatus.COMPLETED;

            _p2pRepository.Save();
            _accountRepository.Save(); 
        }
    }
}
