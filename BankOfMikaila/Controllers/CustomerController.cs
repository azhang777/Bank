using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Response;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    //have the controller talk to the response and handle exceptions as early as possible.
    //this should be where logger is bc of exception handling
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerResponse _customerResponse;
        //logger
        public CustomerController(CustomerResponse customerResponse)
        {
            _customerResponse = customerResponse;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> CreateCustomer([FromBody] CustomerCreateDTO customerCreateDTO)
        {

            try
            {
                return _customerResponse.CreateCustomer(customerCreateDTO);
            }
            catch (Exception ex)
            {
                APIResponse errorResponse = new()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
