using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Models.DTO.Update;
using BankOfMikaila.Response.Format;
using BankOfMikaila.Services;

namespace BankOfMikaila.Response
{
    public class BillResponse
    {
        private readonly BillService _billService; //readonly keyword: can be assigned a value only during its initialization or construction (constructor), value cannot be changed after.
        private readonly IMapper _mapper;

        public BillResponse(BillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        public BillDTO CreateBill(long accountId, BillCreateDTO billCreateDTO) //CreateDTOs and updateDTOs come from requests, they should not be responses, RegularDTOs are used for responses.
        {
            var newBill = _mapper.Map<Bill>(billCreateDTO);
            var result = _mapper.Map<BillDTO>(_billService.CreateBill(accountId, newBill));

            return result;
        }

        public DataResponse GetBill(long billId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status201Created,
                Message = "Success - Bill created",
                Data = _mapper.Map<BillDTO>(_billService.GetBill(billId))
            };

            return successResponse;
        }

        public DataResponse GetBillsByAccount(long accountId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Bills retrieved by account",
                Data = _mapper.Map<IEnumerable<BillDTO>>(_billService.GetBillsByAccount(accountId))
            };

            return successResponse;
        }

        public DataResponse GetBillsByCustomer(long customerId)
        {
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Bills retrieved by customer",
                Data = _mapper.Map<IEnumerable<BillDTO>>(_billService.GetBillsByCustomer(customerId))
            };

            return successResponse;
        }

        public DataResponse UpdateBill(long billId, BillUpdateDTO billUpdateDTO)
        {
            var updatedBill = _mapper.Map<Bill>(billUpdateDTO);
            _billService.UpdateBill(billId, updatedBill);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status200OK,
                Message = "Success - Bill updated"
            };

            return successResponse;
        }

        public DataResponse DeleteBill(long billId)
        {
            _billService.DeleteBill(billId);
            
            DataResponse successResponse = new()
            {
                Code = StatusCodes.Status204NoContent,
                Message = "Success - Bill deleted"
            };

            return successResponse;
        }
    }
}
