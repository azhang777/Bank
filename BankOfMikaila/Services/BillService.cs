using BankOfMikaila.Models;
using BankOfMikaila.Repository.IRepository;
namespace BankOfMikaila.Services
{
    public class BillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IAccountRepository _accountRepository;


        public BillService(IBillRepository billRepository, IAccountRepository accountRepository)
        {
            _billRepository = billRepository;
            _accountRepository = accountRepository;
        }

        public Bill CreateBill(long accountId, Bill newBill)
        {
            newBill.Account = _accountRepository.Get(accountId);
            newBill.AccountId = accountId;
            _billRepository.Create(newBill);
            _billRepository.Save();

            return newBill;
        }

        public Bill GetBill(long billId)
        {
            return _billRepository.Get(billId);
        }

        public IEnumerable<Bill> GetBillsByAccount(long accountId)
        {
            return _billRepository.GetAllFiltered(bill => bill.AccountId == accountId);
        }

        public IEnumerable<Bill> GetBillsByCustomer(long customerId)
        {
            return _billRepository.GetAllFiltered(bill => bill.Account.CustomerId == customerId);
        }

        public Bill UpdateBill(long billId, Bill updatedBill)
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
            var billToDelete = GetBill(billId);
            _billRepository.Remove(billToDelete);
            _billRepository.Save();
        }
    }
}

//REVELATION? HAVE ALL SERVICE METHODS RETURN TRUE ENTITY. HAVE THE RESPONSE LAYER CONVERT TWICE. THEY ARE THE INTERPRETER!