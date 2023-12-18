using System.Transactions;

namespace BankOfMikaila.Models.DTO
{
    public class BillDTO
    {
        public long Id { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string Payee { get; set; }
        public string NickName { get; set; }
        public string CreationDate {  get; set; }
        public string PaymentDate { get; set; }
        public int RecurringDate { get; set; }
        public string UpcomingPaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public long AccountId { get; set; }
    }
}
