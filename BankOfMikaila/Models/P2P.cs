using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfMikaila.Models
{
    public class P2P : Transaction
    {
        [Required]
        public long ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual Account Receiver { get; set; }
    }
}
