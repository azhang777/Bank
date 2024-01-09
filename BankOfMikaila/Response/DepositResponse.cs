using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class DepositResponse
    {
        private readonly DepositService _depositService;
        private readonly IMapper _mapper;

        public DepositResponse(DepositService depositService, IMapper mapper)
        {
            _depositService = depositService;
            _mapper = mapper;
        }

        public DepositDTO CreateDeposit(long accountId, DepositCreateDTO depositCreateDTO)
        {
            var deposit = _mapper.Map<Deposit>(depositCreateDTO);
            var result = _mapper.Map<DepositDTO>(_depositService.CreateDeposit(accountId, deposit));

            return result;
        }

        public DataResponse GetDeposit(long depositId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Deposit retrieved",
                Data = _mapper.Map<DepositDTO>(_depositService.GetDeposit(depositId))
            };

            return successResponse;
        }

        public DataResponse GetDepositsByAccount(long accountId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - All deposits retrieved for account",
                Data = _mapper.Map<IEnumerable<DepositDTO>>(_depositService.GetDepositsByAccount(accountId))
            };

            return successResponse;
        }

        public DataResponse UpdateDeposit(long depositId, DepositUpdateDTO depositUpdateDTO)
        {
            var updatedDeposit = _mapper.Map<Deposit>(depositUpdateDTO);
            _depositService.UpdateDeposit(depositId, updatedDeposit);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Deposit updated",
            };

            return successResponse;
        }

        public DataResponse CancelDeposit(long depositId)
        {
            _depositService.CancelDeposit(depositId);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status204NoContent, //are we canceling or deleting? 200,202,204?
                Message = "Success - Deposit canceled"
            };

            return successResponse; //this is very iffy. check tmr
        }
    }
}
