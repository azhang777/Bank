using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum TransactionType
    {
        [EnumMember(Value = "Withdrawal")]
        WITHDRAWAL = 1,
        [EnumMember(Value = "Deposit")]
        DEPOSIT = 2,
        [EnumMember(Value = "P2P")]
        P2P = 3,
        [EnumMember(Value = "ACTION")]
        DEFAULT = 0
    }
}
