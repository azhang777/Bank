using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace BankOfMikaila.Models
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        [Required]
        public string Payee { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        public string CreationDate { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        [Required]
        public int RecurringDate { get; set; }
        [Required]
        public string UpcomingPaymentDate { get; set; }
        [Required]
        public double PaymentAmount { get; set; }

        public long AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
