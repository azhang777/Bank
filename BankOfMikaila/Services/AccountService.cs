using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;
using System.Collections;
using System.Net;

namespace BankOfMikaila.Services
{
    
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICacheService _cacheService;
        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, ICacheService cacheService)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _cacheService = cacheService;
        }

        public Account CreateAccount(long customerId, Account newAccount)
        {
            var customer = _customerRepository.Get(customerId, customer => customer.Address) ?? throw new CustomerNotFoundException("Customer " + customerId + " not found");
            newAccount.Customer = customer;
            _accountRepository.Create(newAccount);
            _accountRepository.Save();

            var expiryTime = DateTimeOffset.Now.AddSeconds(40);
            _cacheService.SetData($"account{newAccount.Id}", newAccount, expiryTime);
            _cacheService.Invalidate("accounts");

            return newAccount;
        }

        public Account GetAccount(long accountId)
        {
            var cacheData = _cacheService.GetData<Account>("Account");

            if (cacheData != null)
            {
                return cacheData;
            }
            else
            {
                cacheData = _accountRepository.Get(accountId);

                var expiryTime = DateTimeOffset.Now.AddSeconds(40);

                _cacheService.SetData($"account{accountId}", cacheData, expiryTime);
            }

            return _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found"); ; 
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            //check cache data
            var cacheData = _cacheService.GetData<IEnumerable<Account>>("accounts");

            if (cacheData != null && cacheData.Any())
            {
                return cacheData;
            }
            else
            {
                cacheData =  _accountRepository.GetAll();

                var expiryTime = DateTimeOffset.Now.AddSeconds(40);

                _cacheService.SetData("accounts", cacheData, expiryTime);
            }

            var accounts = _accountRepository.GetAll();
            
            if (accounts.Count == 0)
            {
                throw new AccountNotFoundException("No accounts found");
            }

            return accounts;
        }

        public IEnumerable<Account> GetAccountsByCustomer(long customerId)
        {
            var accounts = _accountRepository.GetAllFiltered(account => account.Customer.Id == customerId);

            if (accounts.Count == 0)
            {
                throw new AccountNotFoundException("Accounts for " + customerId + " not found");
            }

            return accounts;
        }

        public Account UpdateAccount(long accountId, Account updatedAccount)
        {
            var existingAccount = GetAccount(accountId); //GetAccount already covers the exception DRY

            existingAccount.AccountType = updatedAccount.AccountType;
            existingAccount.NickName = updatedAccount.NickName;
            existingAccount.Rewards = updatedAccount.Rewards;
            existingAccount.Balance = updatedAccount.Balance;

            _accountRepository.Update(existingAccount);
            _customerRepository.Save();

            _cacheService.RemoveData($"account{accountId}");
            _cacheService.Invalidate("accounts");

            return existingAccount;
        }

        public void DeleteAccount(long accountId)
        {
            var accountToDelete = GetAccount(accountId);

            if (accountToDelete.Balance != 0)
            {
                throw new CustomException("Account cannot be deleted because balance is 0");
            }

            _accountRepository.Remove(accountToDelete);
            _accountRepository.Save();


            _cacheService.RemoveData($"account{accountId}");
            _cacheService.Invalidate("accounts");
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
