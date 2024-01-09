using BankOfMikaila.Models.DTO.Create;

namespace BankOfMikaila.Models.DTO.Update
{
    public class P2PDTO : TransactionDTO
    {
        public long ReceiverId { get; set; }
    }
}
