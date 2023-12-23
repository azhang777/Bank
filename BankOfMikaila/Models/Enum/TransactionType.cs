using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum TransactionType
    {
        [EnumMember(Value = "P2P")]
        P2P,
        [EnumMember(Value = "Deposit")]
        DEPOSIT,
        [EnumMember(Value = "Withdrawal")]
        WITHDRAWAL
    }
}
