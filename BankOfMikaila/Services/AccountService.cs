using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;

namespace BankOfMikaila.Services
{
    
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerService _customerService;
        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, CustomerService customerService)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        public Account CreateAccount(long customerId, Account account)
        {
            account.Customer = _customerService.GetCustomer(customerId);
            _accountRepository.Create(account);
            _accountRepository.Save();
            return account;
        }

        public Account GetAccount(long id)
        {
            return _accountRepository.Get(id, account => account.Customer.Address); //maybe for account, only show the customer id instead of the entire customer object
        }
    }
}
