using BankOfMikaila.Models.Enum;

namespace BankOfMikaila.Models.DTO
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public AccountType AccountType { get; set; }
        public string NickName { get; set; }
        public int Rewards { get; set; }
        public double Balance { get; set; }
        public Customer Customer { get; set; }
    }
}
