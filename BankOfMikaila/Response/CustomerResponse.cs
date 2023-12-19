using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
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

        public DataResponse CreateCustomer(CustomerCreateDTO customerCreateDTO)
        {
            Customer customer = _mapper.Map<Customer>(customerCreateDTO);
            DataResponse successResponse = new()
            {
                Data = _customerService.CreateCustomer(customer),
                Code = StatusCodes.Status201Created,
                Message = "Success - Customer created"
            };

            return successResponse;
        }

        public DataResponse GetCustomer(long id)
        {
            DataResponse successResponse = new()
            {
                Data = _customerService.GetCustomer(id),
                Code = StatusCodes.Status200OK,
                Message = "Success - Customer retrieved"
            };

            return successResponse;
        }

        public DataResponse GetAllCustomers()
        {
            DataResponse successResponse = new()
            {
                Data = _customerService.GetAllCustomers(),
                Code = StatusCodes.Status200OK,
                Message = "Success - All customers retrieved"
            };

            return successResponse;
        }

        public DataResponse UpdateCustomer(long id, CustomerUpdateDTO customerUpdateDTO)
        {
            Customer customer = _mapper.Map< Customer>(customerUpdateDTO);
            DataResponse successResponse = new()
            {
                Data = _customerService.UpdateCustomer(id, customer),
                Code = StatusCodes.Status202Accepted,
                Message = "Success - Customer updated"
            };

            return successResponse;
        }
    }
}
