using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfMikaila.Models
{
    public class P2P : Transaction
    {
        [Required]
        [ForeignKey("ReceiverId")]
        public long ReceiverId { get; set; }
        public virtual Account Receiver { get; set; }
    }
}
