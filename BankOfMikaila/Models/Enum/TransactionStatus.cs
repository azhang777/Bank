using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum TransactionStatus
    {
        [EnumMember(Value = "PENDING")]
        PENDING = 1,
        [EnumMember(Value = "CANCELED")]
        CANCELED = -1,
        [EnumMember(Value = "COMPLETED")]
        COMPLETED = 2,
        [EnumMember(Value = "RECURRING")]
        RECURRING = 0
    }
}
