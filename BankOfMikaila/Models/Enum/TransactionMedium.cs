using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum TransactionMedium
    {
        [EnumMember(Value = "BALANCE")]
        BALANCE = 1,
        [EnumMember(Value = "REWARDS")]
        REWARDS = 2
    }
}
