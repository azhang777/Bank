using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class WithdrawalResponse
    {
        private readonly WithdrawalService _withdrawalService;
        private readonly IMapper _mapper;

        public WithdrawalResponse(WithdrawalService withdrawalService, IMapper mapper)
        {
            _withdrawalService = withdrawalService;
            _mapper = mapper;
        }

        public WithdrawalDTO CreateWithdrawal(long accountId, WithdrawalCreateDTO withdrawalCreateDTO)
        {
            var withdrawal = _mapper.Map<Withdrawal>(withdrawalCreateDTO);
            var result = _mapper.Map<WithdrawalDTO>(_withdrawalService.CreateWithdrawal(accountId, withdrawal));

            return result;
        }

        public DataResponse GetWithdrawal(long withdrawalId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Withdrawal retrieved",
                Data = _mapper.Map<WithdrawalDTO>(_withdrawalService.GetWithdrawal(withdrawalId))
            };

            return successResponse;
        }

        public DataResponse GetWithdrawalsByAccount(long accountId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All deposits retrieved for account",
                Data = _mapper.Map<IEnumerable<WithdrawalDTO>>(_withdrawalService.GetWithdrawalsByAccount(accountId))
            };

            return successResponse;
        }

        public DataResponse UpdateWithdrawal(long withdrawalId, WithdrawalUpdateDTO withdrawalUpdateDTO)
        {
            var updatedWithdrawal = _mapper.Map<Withdrawal>(withdrawalUpdateDTO);
            _withdrawalService.UpdateWithdrawal(withdrawalId, updatedWithdrawal);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Withdrawal updated",
            };

            return successResponse;
        }

        public DataResponse CancelWithdrawal(long withdrawalId)
        {
            _withdrawalService.CancelWithdrawal(withdrawalId);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status204NoContent,
                Message = "Success - Withdrawal canceled"
            };

            return successResponse; //this is very iffy. check tmr
        }
    }
}
