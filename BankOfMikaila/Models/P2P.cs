using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfMikaila.Models
{
    public class P2P : Transaction
    {
        [ForeignKey("Account2_Id")]
        public long Account2_Id { get; set; }
        public virtual Account Account2 { get; set; }
    }
}
