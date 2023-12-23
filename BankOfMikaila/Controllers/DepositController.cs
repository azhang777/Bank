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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateDeposit(long accountId, [FromBody] DepositCreateDTO depositCreateDTO)
        {   
            try
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
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpGet("deposits/{depositId}", Name = "GetDeposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetWithdrawal(long depositId)
        {
            try
            {
                return _depositResponse.GetDeposit(depositId);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}
