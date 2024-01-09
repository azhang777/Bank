using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DepositController : ControllerBase
    {
        private readonly DepositResponse _depositResponse;

        public DepositController(DepositResponse depositResponse)
        {
            _depositResponse = depositResponse;
        }

        [HttpPost("accounts/{accountId}/deposits")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateDeposit(long accountId, [FromBody] DepositCreateDTO depositCreateDTO)
        {
            var deposit = _depositResponse.CreateDeposit(accountId, depositCreateDTO);

            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Deposit created",
                Data = deposit
            };

            return CreatedAtRoute("GetDeposit", new { depositId = deposit.Id }, successResponse);
        }

        [HttpGet("deposits/{depositId}", Name = "GetDeposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetWithdrawal(long depositId)
        {
            return _depositResponse.GetDeposit(depositId);
        }

        [HttpGet("accounts/{accountId}/deposits", Name = "GetDepositsByAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetDepositsByAccount(long accountId)
        {
            return _depositResponse.GetDepositsByAccount(accountId);
        }

        [HttpPut("deposits/{depositId}", Name = "UpdateDeposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> UpdateDeposit(long depositId, [FromBody] DepositUpdateDTO depositUpdateDTO)
        {
            return _depositResponse.UpdateDeposit(depositId, depositUpdateDTO);
        }

        [HttpDelete("deposits/{depositId}", Name = "CancelDeposit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CancelWithdrawal(long depositId)
        {
            _depositResponse.CancelDeposit(depositId);

            return NoContent();
        }
    }
}
