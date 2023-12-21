using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
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
        private readonly IAddressRepository _addressRepository;
        
        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _addressRepository = addressRepository;
        }

        public Customer CreateCustomer(Customer customer)
        {
            _customerRepository.Create(customer);
            _customerRepository.Save();
            return customer;
        }

        public Customer GetCustomer(long customerId)
        {
            return _customerRepository.Get(customerId, customer => customer.Address);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll(customer => customer.Address);
        }

        public Customer UpdateCustomer(long customerId, Customer updatedCustomer)
        {
            var existingCustomer = GetCustomer(customerId);

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;

            for (int i = 0; i < updatedCustomer.Address.Count; i++)
            {
                if (i < existingCustomer.Address.Count)
                {
                    existingCustomer.Address[i].StreetName = updatedCustomer.Address[i].StreetName;
                    existingCustomer.Address[i].StreetNumber = updatedCustomer.Address[i].StreetNumber;
                    existingCustomer.Address[i].City = updatedCustomer.Address[i].City;
                    existingCustomer.Address[i].State = updatedCustomer.Address[i].State;
                    existingCustomer.Address[i].ZipCode = updatedCustomer.Address[i].ZipCode;
                }
                else
                {
                    existingCustomer.Address.Add(updatedCustomer.Address[i]);
                }
            }

            //foreach (var address in existingCustomer.Address)
            //{
            //    var updatedAddress = updatedCustomer.Address.FirstOrDefault(x => x.Id == address.Id);
            //    address.StreetNumber = updatedAddress.StreetNumber;
            //    address.StreetName = updatedAddress.StreetName;
            //    address.City = updatedAddress.City;
            //    address.State = updatedAddress.State;
            //    address.ZipCode = updatedAddress.ZipCode;
            //    _addressRepository.Update(address);
            //}

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
        public Customer GetCustomerByAccount(long accountId)
        {
            long customerId = _accountRepository.Get(accountId).CustomerId; //works? if we get accountId 5 we get customerId1, but the object is null
            var customer = _customerRepository.Get(customerId, customer => customer.Address);
            return customer;
        }
        //GetAllCustomerAccounts in account service
    }
}
/*
 *      with DTO
               private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public CustomerDTO CreateCustomer(Customer newCustomer)
        {
            _customerRepository.Create(newCustomer);
            _customerRepository.Save();

            var customer = _mapper.Map<CustomerDTO>(newCustomer);
            return customer;
        }

        public CustomerDTO GetCustomer(long customerId)
        {
            var customer = _mapper.Map<CustomerDTO>(_customerRepository.Get(customerId, customer => customer.Address));
            return customer;
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            IEnumerable<Customer> retrieveCustomers = _customerRepository.GetAll(customer => customer.Address);
            IEnumerable<CustomerDTO> resultCustomers = _mapper.Map<IEnumerable<CustomerDTO>>(retrieveCustomers);

            return resultCustomers;
        }

        public CustomerDTO UpdateCustomer(long customerId, Customer updatedCustomer)
        {
            var existingCustomer = _customerRepository.Get(customerId, customer => customer.Address);

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;
            existingCustomer.Address = updatedCustomer.Address;

            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();

            var resultCustomer = _mapper.Map<CustomerDTO>(existingCustomer);
            return resultCustomer;
        }

We do not need to abstract customer by its customerDTO, we need to return everything from customer.
*/

/* WITHOUT DTO
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

        public Customer GetCustomer(long customerId)
        {
            return _customerRepository.Get(customerId, customer => customer.Address);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll(customer => customer.Address);
        }

        public Customer UpdateCustomer(long customerId, Customer updatedCustomer)
        {
            var existingCustomer = GetCustomer(customerId);

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;

            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();
            return existingCustomer;
        }
*/