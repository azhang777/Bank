using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum AccountType
    {
        [EnumMember(Value = "SAVINGS")]
        SAVINGS,
        [EnumMember(Value = "CHECKINGS")]
        CHECKINGS,
        [EnumMember(Value = "CREDIT")]
        CREDIT
    }
}