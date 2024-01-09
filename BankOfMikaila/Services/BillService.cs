﻿using BankOfMikaila.Exceptions;
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
            newBill.Account = _accountRepository.Get(accountId) ?? throw new AccountNotFoundException("Account " + accountId + " not found"); ;
            newBill.AccountId = accountId;
            _billRepository.Create(newBill);
            _billRepository.Save();

            return newBill;
        }

        public Bill GetBill(long billId)
        {
            return _billRepository.Get(billId) ?? throw new BillNotFoundException("Bill " + billId + " not found");
        }

        public IEnumerable<Bill> GetBillsByAccount(long accountId)
        {
            var bills = _billRepository.GetAllFiltered(bill => bill.AccountId == accountId);

            if (bills.Count == 0)
            {
                throw new BillNotFoundException("No bills found for account " + accountId);
            }

            return bills; 
        }

        public IEnumerable<Bill> GetBillsByCustomer(long customerId)
        {
            var bills = _billRepository.GetAllFiltered(bill => bill.Account.CustomerId == customerId);

            if (bills.Count == 0)
            {
                throw new BillNotFoundException("No bills found for customer " + customerId);
            }

            return bills;
        }

        public Bill UpdateBill(long billId, Bill updatedBill)
        {
            var existingBill = GetBill(billId);

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
            var billToDelete = GetBill(billId); //no need to throw an exception bc the GetBill already covers it
            _billRepository.Remove(billToDelete);
            _billRepository.Save();
        }
    }
}

//REVELATION? HAVE ALL SERVICE METHODS RETURN TRUE ENTITY. HAVE THE RESPONSE LAYER CONVERT TWICE. THEY ARE THE INTERPRETER!