using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/")]
    public class P2PController : ControllerBase
    {
        private readonly P2PResponse _p2pResponse;

        public P2PController(P2PResponse p2pResponse)
        {
            _p2pResponse = p2pResponse;
        }

        [HttpPost("accounts/{accountId}/p2p")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateP2P(long accountId, [FromBody] P2PCreateDTO p2PCreateDTO)
        {
            try
            {
                var p2p = _p2pResponse.CreateP2P(accountId, p2PCreateDTO);

                DataResponse successResponse = new()
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Success - P2P Created",
                    Data = p2p
                };

                return CreatedAtRoute("GetP2P", new { p2pId = p2p.Id }, successResponse);
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

        [HttpGet("p2p/{p2pId}", Name = "GetP2P")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetP2P(long p2pId)
        {
            try
            {
                return _p2pResponse.GetP2P(p2pId);
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
