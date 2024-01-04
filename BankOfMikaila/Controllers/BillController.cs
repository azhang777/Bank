using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/")]
    [EnableCors("MyPolicy")]
    public class BillController : ControllerBase
    {
        private readonly BillResponse _billResponse;

        public BillController(BillResponse billResponse)
        {
            _billResponse = billResponse;
        }

        [HttpPost("accounts/{accountId}/bills")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> CreateBill(long accountId, [FromBody] BillCreateDTO billCreateDTO)
        {
            var bill = _billResponse.CreateBill(accountId, billCreateDTO);

            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Bill created",
                Data = bill
            };

            return CreatedAtRoute("GetBill", new { billId = bill.Id }, successResponse);
        }

        [HttpGet("bills/{billId}", Name = "GetBill")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetBill(long billId)
        {
            return _billResponse.GetBill(billId);
        }

        [HttpGet("accounts/{accountId}/bills", Name = "GetBillsByAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetBillsByAccount(long accountId)
        {
            return _billResponse.GetBillsByAccount(accountId);
        }

        [HttpGet("customers/{customerId}/bills", Name = "GetBillsByCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetBillsByCustomer(long customerId)
        {
            return _billResponse.GetBillsByCustomer(customerId);
        }

        [HttpPut("bills/{billId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> UpdateBill(long billId, [FromBody] BillUpdateDTO billUpdateDTO)
        {
            return _billResponse.UpdateBill(billId, billUpdateDTO);
        }

        [HttpDelete("bills/{billId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> DeleteBill(long billId)
        {
            _billResponse.DeleteBill(billId);

            return NoContent();
        }
    }
}
