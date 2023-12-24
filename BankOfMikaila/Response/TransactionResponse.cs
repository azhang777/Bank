using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class TransactionResponse
    {
        private readonly TransactionService _transactionService;
        private readonly IMapper _mapper;
        
        public TransactionResponse(TransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public DataResponse GetAllTransactions()
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All transactions retrieved",
                Data = _mapper.Map<IEnumerable<Transaction>>(_transactionService.GetAllTransactions())
            };

            return successResponse;
        }
    }
}
