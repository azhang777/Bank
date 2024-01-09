using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WithdrawalController : ControllerBase
    {
        private readonly WithdrawalResponse _withdrawalResponse;
        
        public WithdrawalController(WithdrawalResponse withdrawalResponse)
        {
            _withdrawalResponse = withdrawalResponse;
        }

        [HttpPost("accounts/{accountId}/withdrawals")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateWithdrawal(long accountId, [FromBody] WithdrawalCreateDTO withdrawalCreateDTO)
        {
            var withdrawal = _withdrawalResponse.CreateWithdrawal(accountId, withdrawalCreateDTO);

            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Withdrawal created",
                Data = withdrawal
            };

            return CreatedAtRoute("GetWithdrawal", new { withdrawalId = withdrawal.Id }, successResponse);
        }

        [HttpGet("withdrawals/{withdrawalId}", Name = "GetWithdrawal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetWithdrawal(long withdrawalId)
        {
            return _withdrawalResponse.GetWithdrawal(withdrawalId);
        }

        [HttpGet("accounts/{accountId}/withdrawals", Name = "GetWithdrawalsByAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetWithdrawalsByAccount(long accountId)
        {
            return _withdrawalResponse.GetWithdrawalsByAccount(accountId);
        }

        [HttpPut("withdrawals/{withdrawalId}", Name = "UpdateWithdrawal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> UpdateWithdrawal(long withdrawalId, [FromBody] WithdrawalUpdateDTO withdrawalUpdateDTO)
        {
            return _withdrawalResponse.UpdateWithdrawal(withdrawalId, withdrawalUpdateDTO);
        }

        [HttpDelete("withdrawals/{withdrawalId}", Name = "CancelWithdrawal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CancelWithdrawal(long withdrawalId)
        {
            _withdrawalResponse.CancelWithdrawal(withdrawalId);

            return NoContent();
        }
    }
}
