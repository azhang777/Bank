﻿using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Repository.IRepository;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    //have the controller talk to the response and handle exceptions as early as possible.
    //this should be where logger is bc of exception handling
    
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerResponse _customerResponse;
        private readonly AccountResponse _accountResponse;
        //logger
        public CustomerController(CustomerResponse customerResponse, AccountResponse accountResponse)
        {
            _customerResponse = customerResponse;
            _accountResponse = accountResponse;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateCustomer([FromBody] CustomerCreateDTO customerCreateDTO)
        {
            var customer = _customerResponse.CreateCustomer(customerCreateDTO);

            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Customer created",
                Data = customer
            };

            return CreatedAtRoute("GetCustomer", new { customerId = customer.Id }, successResponse); //new customerId needs to match the argument in GetCustomer customerId, it is not id!
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetCustomer(long customerId)
        {
            return _customerResponse.GetCustomer(customerId);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetAllCustomers()
        {
            return _customerResponse.GetAllCustomers();
        }

        [HttpPut("{customerId}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> UpdateCustomer(long customerId, [FromBody] CustomerUpdateDTO customerUpdateDTO)
        {
            return _customerResponse.UpdateCustomer(customerId, customerUpdateDTO);
        }

        [HttpPost("{customerId}/accounts", Name = "CreateAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateAccount(long customerId, [FromBody] AccountCreateDTO accountCreateDTO)
        {
            var accountDTO = _accountResponse.CreateAccount(customerId, accountCreateDTO);

            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Account created",
                Data = accountDTO
            };

            return CreatedAtRoute("GetAccount", new { accountId = accountDTO.Id }, successResponse);
        }

        [HttpGet("{customerId}/accounts", Name = "GetAccountsByCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetAccountsByCustomer(long customerId)
        {
            return _accountResponse.GetAccountsByCustomer(customerId);
        }
    }
}
