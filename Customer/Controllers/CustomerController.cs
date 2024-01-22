using Customer.Models;
using Customer.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController: ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<CustomerDTO>),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<DataResponse<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO createCustomer)
        {
            return null;
        }

        [HttpGet("${customerId}")]
        [ProducesResponseType(typeof(DataResponse<CustomerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<DataResponse<CustomerDTO>> GetCustomer(long customerId)
        {
            return null;
        }

        [HttpGet]
        [ProducesResponseType(typeof(DataResponse<IEnumerable<CustomerDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<DataResponse<IEnumerable<CustomerDTO>>> GetAllCustomers() 
        {
            return null;
        }

        [HttpPut("{customerId}", Name = "UpdateCustomer")]
        [ProducesResponseType(typeof(DataResponse<>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<DataResponse<CustomerDTO>>  UpdateCustomer(long customerId, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            return null;
        }
    }
}
