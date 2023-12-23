﻿using BankOfMikaila.Models.DTO.Update;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateWithdrawal(long accountId, [FromBody] WithdrawalCreateDTO withdrawalCreateDTO)
        {
            try
            {
                var withdrawal = _withdrawalResponse.CreateWithdrawal(accountId, withdrawalCreateDTO);
                DataResponse successResponse = new()
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Success - Withdrawal created",
                    Data = withdrawal
                };

                return CreatedAtRoute("GetWithdrawal", new {withdrawalId = withdrawal.Id}, successResponse);
            }
            catch(Exception ex)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        [HttpGet("withdrawals/{withdrawalId}", Name = "GetWithdrawal")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetWithdrawal(long withdrawalId)
        {
            try
            {
                return _withdrawalResponse.GetWithdrawal(withdrawalId);
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
