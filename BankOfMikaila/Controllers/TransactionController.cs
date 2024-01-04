using BankOfMikaila.Response;
using BankOfMikaila.Response.Format;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BankOfMikaila.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [EnableCors("MyPolicy")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionResponse _transactionResponse;

        public TransactionController(TransactionResponse transactionResponse)
        {
            _transactionResponse = transactionResponse;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataResponse> GetAllTransactions()
        {
            return _transactionResponse.GetAllTransactions();
        }
    }
}
