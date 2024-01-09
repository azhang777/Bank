using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class P2PResponse
    {
        private readonly P2PService _p2pService;
        private readonly IMapper _mapper;

        public P2PResponse(P2PService p2PService, IMapper mapper)
        {
            _p2pService = p2PService;
            _mapper = mapper;
        }

        public P2PDTO CreateP2P(long accountId, P2PCreateDTO p2pCreateDTO)
        {
            var p2p = _mapper.Map<P2P>(p2pCreateDTO);
            var result =  _mapper.Map<P2PDTO>(_p2pService.CreateP2P(accountId, p2p));

            return result;
        }

        public DataResponse GetP2P(long p2pId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - P2P Retrieved",
                Data = _mapper.Map<P2PDTO>(_p2pService.GetP2P(p2pId))
            };

            return successResponse;
        }
    }
}
