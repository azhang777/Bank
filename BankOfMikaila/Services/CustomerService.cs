using BankOfMikaila.Exceptions;
using BankOfMikaila.Models;
using BankOfMikaila.Repository;
using BankOfMikaila.Repository.IRepository;


namespace BankOfMikaila.Services
{
    //have service do business logic and use repository to manipulate database and return necessary data to the response
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICacheService _cacheService;
        public CustomerService(ICustomerRepository customerRepository, IAccountRepository accountRepository, ICacheService cacheService)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _cacheService = cacheService;
        }

        public Customer CreateCustomer(Customer customer)
        {
            _customerRepository.Create(customer);

            _customerRepository.Save();

            var expiryTime = DateTimeOffset.Now.AddSeconds(40);
            _cacheService.SetData($"customer{customer.Id}", customer, expiryTime);
            _cacheService.Invalidate("customers");

            return customer;
        }

        public Customer GetCustomer(long customerId)
        {
            var cacheData = _cacheService.GetData<Customer>($"customer{customerId}");

            if (cacheData != null)
            {
                return cacheData;
            }
            else
            {
                cacheData = _customerRepository.Get(customerId, customer => customer.Address);

                var expiryTime = DateTimeOffset.Now.AddSeconds(40);

                _cacheService.SetData($"customer{customerId}", cacheData, expiryTime);
            }

            return _customerRepository.Get(customerId, customer => customer.Address) ?? throw new CustomerNotFoundException("Customer " + customerId + " not found");
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var cacheData = _cacheService.GetData<IEnumerable<Customer>>("customers");

            if (cacheData != null && cacheData.Any())
            {
                return cacheData;
            }
            else
            {
                cacheData = _customerRepository.GetAll();

                var expiryTime = DateTimeOffset.Now.AddSeconds(40);

                _cacheService.SetData("customers", cacheData, expiryTime);
            }

            var customers = _customerRepository.GetAll(customer => customer.Address);

            if (customers.Count == 0)
            {
                throw new CustomerNotFoundException("No customers found");
            }

            return customers;
        }

        public Customer UpdateCustomer(long customerId, Customer updatedCustomer)
        {
            var existingCustomer = GetCustomer(customerId);

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;

            for (int i = 0; i < updatedCustomer.Address.Count; i++)
            {
                if (i < existingCustomer.Address.Count) //handle updating the addressses of the customer. Overrides existing addresses with the updated addresses (in order)
                {
                    existingCustomer.Address[i].StreetName = updatedCustomer.Address[i].StreetName; 
                    existingCustomer.Address[i].StreetNumber = updatedCustomer.Address[i].StreetNumber;
                    existingCustomer.Address[i].City = updatedCustomer.Address[i].City;
                    existingCustomer.Address[i].State = updatedCustomer.Address[i].State;
                    existingCustomer.Address[i].ZipCode = updatedCustomer.Address[i].ZipCode;
                }
                else //updated customer could have more addresses than its previous state, if so add the new addresses to the list
                {
                    existingCustomer.Address.Add(updatedCustomer.Address[i]);
                }
            }

            _customerRepository.Update(existingCustomer);
            _customerRepository.Save();

            _cacheService.Invalidate("customers");

            return existingCustomer;
        }

        //public bool DeleteCustomer(long id)
        //{
        //    var customerToDelete = GetCustomer(id);

        //    _customerRepository.Remove(customerToDelete);
        //    return _customerRepository.Save(); 
        //}


        public Customer GetCustomerByAccount(long accountId)
        {
            var account = _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found");
            long customerId = account.CustomerId; 
            var customer = _customerRepository.Get(customerId, customer => customer.Address);

            return customer;
        }
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