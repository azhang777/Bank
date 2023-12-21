using AutoMapper;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Models.DTO.Create;
using BankOfMikaila.Repository.IRepository;
using Microsoft.Identity.Client;

namespace BankOfMikaila.Services
{
    public class BillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public BillService(IBillRepository billRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public BillDTO CreateBill(long accountId, Bill newBill)
        {
            var account = _accountRepository.Get(accountId);
            newBill.Account = account;
            newBill.AccountId = accountId;
            _billRepository.Create(newBill);
            _billRepository.Save();

            var bill = _mapper.Map<BillDTO>(newBill);
            return bill;
        }

        public BillDTO GetBill(long billId)
        {
            var bill = _mapper.Map<BillDTO>(_billRepository.Get(billId));
            return bill;
        }

        public IEnumerable<BillDTO> GetAllBillsByAccount(long accountId)
        {
            var billsFromAccount = _mapper.Map<IEnumerable<BillDTO>>(_billRepository.GetAllFiltered(bill => bill.AccountId == accountId));
            return billsFromAccount;
        }

        public IEnumerable<BillDTO> GetBillsByCustomer(long customerId)
        {
            var billsFromCustomer = _mapper.Map<IEnumerable<BillDTO>>(_billRepository.GetAllFiltered(bill => bill.Account.CustomerId == customerId));
            return billsFromCustomer;
        }

        public Bill updateBill(long billId, Bill updatedBill)
        {
            var existingBill = _billRepository.Get(billId);

            existingBill.TransactionStatus = updatedBill.TransactionStatus;
            existingBill.Payee = updatedBill.Payee;
            existingBill.NickName = updatedBill.NickName; 
            existingBill.CreationDate = updatedBill.CreationDate;
            existingBill.PaymentDate = updatedBill.PaymentDate;
            existingBill.RecurringDate = updatedBill.RecurringDate;
            existingBill.UpcomingPaymentDate = updatedBill.UpcomingPaymentDate;
            existingBill.PaymentAmount = updatedBill.PaymentAmount;

            _billRepository.Update(existingBill);
            _billRepository.Save();

            return existingBill;
        }
        public void DeleteBill(long billId)
        {
            var billToDlete = _billRepository.Get(billId);
            _billRepository.Remove(billToDlete);
            _billRepository.Save();
        }
    }
}

//REVELATION? HAVE ALL SERVICE METHODS RETURN TRUE ENTITY. HAVE THE RESPONSE LAYER CONVERT TWICE. THEY ARE THE INTERPRETER!