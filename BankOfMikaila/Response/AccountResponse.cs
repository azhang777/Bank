using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class AccountResponse
    {
        private readonly AccountService _accountService;
        private readonly IMapper _mapper;

        public AccountResponse(AccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public DataResponse CreateAccount(long customerId, AccountCreateDTO accountCreateDTO)
        {
            var account = _mapper.Map<Account>(accountCreateDTO);
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Account created",
                Data = _accountService.CreateAccount(customerId, account)
            };

            return successResponse;
        }

        public DataResponse GetAccount(long id)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Account retrieved",
                Data = _accountService.GetAccount(id)
            };

            return successResponse;
        }
    }

}
