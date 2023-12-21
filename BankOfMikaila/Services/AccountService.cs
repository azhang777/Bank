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

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public Account CreateAccount(long customerId, Account newAccount)
        {
            newAccount.Customer = _customerRepository.Get(customerId, customer => customer.Address);
            _accountRepository.Create(newAccount);
            _accountRepository.Save();

            return newAccount;
        }

        public Account GetAccount(long accountId)
        {
            return _accountRepository.Get(accountId);
        }

        public IEnumerable<Account> GetAllAccounts()
        { 
            return _accountRepository.GetAll();
        }

        public IEnumerable<Account> GetAccountsByCustomer(long customerId)
        {
            return _accountRepository.GetAllFiltered(account => account.Customer.Id == customerId);
        }

        public Account UpdateAccount(long accountId, Account updatedAccount)
        {
            var existingAccount = _accountRepository.Get(accountId);

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
