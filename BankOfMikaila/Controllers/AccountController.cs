
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AccountResponse _accountResponse;
        private readonly CustomerResponse _customerResponse;
        //logger
        public AccountController(AccountResponse accountResponse, CustomerResponse customerResponse)
        {
            _accountResponse = accountResponse;
            _customerResponse = customerResponse;
        }

        [HttpGet("{accountId}", Name = "GetAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetAccount(long accountId)
        {
            return _accountResponse.GetAccount(accountId);
        }

        [HttpGet("{accountId}/customer", Name = "GetCustomerByAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetCustomerByAccount(long accountId)
        {
            return _customerResponse.GetCustomerByAccount(accountId);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetAllAccounts()
        {
            return _accountResponse.GetAllAccounts();
        }

        [HttpPut("{accountId}", Name = "UpdateAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> UpdateAccount(long accountId, [FromBody] AccountUpdateDTO accountUpdateDTO)
        {
            return _accountResponse.UpdateAccount(accountId, accountUpdateDTO);
        }

        [HttpDelete("{accountId}", Name = "DeleteAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAccount(long accountId) 
        {
            _accountResponse.DeleteAccount(accountId);
            return NoContent();
        }
    }
}
