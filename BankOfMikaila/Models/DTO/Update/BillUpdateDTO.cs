using BankOfMikaila.Models.Enum;

namespace BankOfMikaila.Models.DTO.Update
{
    public class BillUpdateDTO
    {
        public TransactionStatus TransactionStatus { get; set; }
        public string Payee { get; set; }
        public string NickName { get; set; }
        public string CreationDate { get; set; }
        public string PaymentDate { get; set; }
        public int RecurringDate { get; set; }
        public string UpcomingPaymentDate { get; set; }
        public double PaymentAmount { get; set; }
    }
}
