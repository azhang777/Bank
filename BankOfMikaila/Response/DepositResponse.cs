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
    }
}
