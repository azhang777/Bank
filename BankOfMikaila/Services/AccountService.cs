using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;
using System.Net;

namespace BankOfMikaila.Services
{
    
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public Account CreateAccount(long customerId, Account newAccount)
        {
            var customer = _customerRepository.Get(customerId, customer => customer.Address) ?? throw new CustomerNotFoundException("Customer " + customerId + " not found");
            newAccount.Customer = customer;
            _accountRepository.Create(newAccount);
            _accountRepository.Save();

            return newAccount;
        }

        public Account GetAccount(long accountId)
        {
            return _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found"); ; 
        }

        public IEnumerable<Account> GetAllAccounts()
        { 
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

            return existingAccount;
        }

        public void DeleteAccount(long accountId)
        {
            var accountToDelete = GetAccount(accountId);
            
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
