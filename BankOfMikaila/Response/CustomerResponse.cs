using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;
using Microsoft.AspNetCore.Mvc;


namespace BankOfMikaila.Response
{
    //have the response talk to the service and transform dto into entity for proper response to controller
    public class CustomerResponse
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerResponse(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public Customer CreateCustomer(CustomerCreateDTO customerCreateDTO)
        {
            Customer newCustomer = _mapper.Map<Customer>(customerCreateDTO);
            //DataResponse successResponse = new()
            //{
            //    Code = StatusCodes.Status201Created,
            //    Message = "Success - Customer created",
            //    Data = _customerService.CreateCustomer(newCustomer)
            //};

            return _customerService.CreateCustomer(newCustomer);
        }

        public DataResponse GetCustomer(long customerId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Customer retrieved",
                Data = _customerService.GetCustomer(customerId)
            };

            return successResponse;
        }

        public DataResponse GetCustomerByAccount(long accountId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Customer retrieved by account",
                Data = _customerService.GetCustomerByAccount(accountId)
            };

            return successResponse;
        }

        public DataResponse GetAllCustomers()
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All customers retrieved",
                Data = _customerService.GetAllCustomers(),
            };

            return successResponse;
        }

        public DataResponse UpdateCustomer(long customerId, CustomerUpdateDTO customerUpdateDTO)
        {
            Customer updatedCustomer = _mapper.Map<Customer>(customerUpdateDTO);
            _customerService.UpdateCustomer(customerId, updatedCustomer);
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Customer updated",
            };

            return successResponse;
        }
    }
}
