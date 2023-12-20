using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BankOfMikaila.Services
{
    //have service do business logic and use repository to manipulate database and return necessary data to the response
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public Customer CreateCustomer(Customer customer)
        {
                _customerRepository.Create(customer);
                _customerRepository.Save();
                return customer;
        }

        public Customer GetCustomer(long id)
        {
            return _customerRepository.Get(id, customer => customer.Address);
        }

        public IEnumerable<Customer> GetAllCustomers() 
        { 
            return _customerRepository.GetAll(customer => customer.Address);
        }

        public Customer UpdateCustomer(long id, Customer customer)
        {
            var existingCustomer = GetCustomer(id);

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;

            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();
            return existingCustomer;
        }

        //public bool DeleteCustomer(long id)
        //{
        //    var customerToDelete = GetCustomer(id);

        //    _customerRepository.Remove(customerToDelete);
        //    return _customerRepository.Save(); 
        //}

        //GetCustomerByAccountId
        //GetAllCustomerAccounts
    }
}
