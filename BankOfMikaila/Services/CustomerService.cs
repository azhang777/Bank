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
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public Customer CreateCustomer(Customer customer)
        {
                _customerRepository.Create(customer);
                _customerRepository.Save();
                return customer;
        }
    }
}
