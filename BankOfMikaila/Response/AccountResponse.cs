using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
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

        public AccountDTO CreateAccount(long customerId, AccountCreateDTO accountCreateDTO)
        {
            var newAccount = _mapper.Map<Account>(accountCreateDTO);
            //DataResponse successResponse = new()
            //{
            //    Code = StatusCodes.Status201Created,
            //    Message = "Success - Account created",
            //    Data = _accountService.CreateAccount(customerId, newAccount)
            //};

            return _accountService.CreateAccount(customerId, newAccount);
        }

        public DataResponse GetAccount(long accountId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Account retrieved",
                Data = _accountService.GetAccount(accountId)
            };

            return successResponse;
        }

        public DataResponse GetAllAccounts()
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All accounts retrieved",
                Data = _accountService.GetAllAccounts()
            };

            return successResponse;
        }

        public DataResponse GetAccountsByCustomer(long customerId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All accounts for customer retrieved",
                Data = _accountService.GetAccountsByCustomer(customerId)
            };

            return successResponse;
        }

        public DataResponse UpdateAccount(long accountId, AccountUpdateDTO accountUpdateDTO)
        {
            var updatedAccount = _mapper.Map<Account>(accountUpdateDTO);
            _accountService.UpdateAccount(accountId, updatedAccount);
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Account updated",
            };

            return successResponse;
        }

        public DataResponse DeleteAccount(long accountId)
        {
            _accountService.DeleteAccount(accountId);
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status204NoContent,
                Message = "Success - Account deleted"
            };

            return successResponse;
        }

        
    }

}
