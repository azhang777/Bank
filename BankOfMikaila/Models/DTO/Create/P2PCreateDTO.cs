using BankOfMikaila.Models.DTO.Create;

namespace BankOfMikaila.Models.DTO.Update
{
    public class P2PCreateDTO : TransactionCreateDTO
    {
        public long ReceiverId { get; set; }
    }
}
