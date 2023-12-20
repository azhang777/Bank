using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Repository.IRepository;
using System.Linq.Expressions;

namespace BankOfMikaila.Services
{
    
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public AccountDTO CreateAccount(long customerId, Account newAccount)
        {
            newAccount.Customer = _customerRepository.Get(customerId, customer => customer.Address);
            _accountRepository.Create(newAccount);
            _accountRepository.Save();

            var account = _mapper.Map<AccountDTO>(newAccount); //create DTO after account is saved to db to get its assigned id
            return account;
        }

        public AccountDTO GetAccount(long accountId)
        {
            var account = _mapper.Map<AccountDTO>(_accountRepository.Get(accountId));
            return account; //maybe for account, only show the customer id instead of the entire customer object
        }

        public IEnumerable<AccountDTO> GetAllAccounts() 
        {
            IEnumerable<Account> retrieveAccounts = _accountRepository.GetAll();
            IEnumerable<AccountDTO> resultAccounts = _mapper.Map<IEnumerable<AccountDTO>>(retrieveAccounts);

            return resultAccounts;
        }

        public IEnumerable<AccountDTO> GetAccountsByCustomer(long customerId)
        {
            
            IEnumerable<Account> retrieveAccounts = _accountRepository.GetAllFiltered(account => account.Customer.Id == customerId); //may not work
            IEnumerable<AccountDTO> resultAccounts = _mapper.Map<IEnumerable<AccountDTO>>(retrieveAccounts);

            return resultAccounts;
        }

        public AccountDTO UpdateAccount(long accountId, Account updatedAccount)
        {
            var existingAccount = _accountRepository.Get(accountId);

            existingAccount.AccountType = updatedAccount.AccountType;
            existingAccount.NickName = updatedAccount.NickName;
            existingAccount.Rewards = updatedAccount.Rewards;
            existingAccount.Balance = updatedAccount.Balance;
           
            _accountRepository.Update(existingAccount);
            _customerRepository.Save();

            var resultAccount = _mapper.Map<AccountDTO>(existingAccount);
            return resultAccount;
        }

        public void DeleteAccount(long accountId)
        {
            var accountToDelete = _accountRepository.Get(accountId);
            _accountRepository.Remove(accountToDelete);
            _accountRepository.Save();
        }

        //public bool DeleteCustomer(long id)
        //{
        //    var customerToDelete = GetCustomer(id);

        //    _customerRepository.Remove(customerToDelete);
        //    return _customerRepository.Save(); 
        //}
        //Get customer that owns the specified account
    }
}
