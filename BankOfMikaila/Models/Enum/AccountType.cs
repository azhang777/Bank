using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BankOfMikaila.Models.Enum
{
    public enum AccountType
    {
        [EnumMember(Value = "SAVINGS")]
        SAVINGS = 1,
        [EnumMember(Value = "CHECKINGS")]
        CHECKINGS = 2,
        [EnumMember(Value = "CREDIT")]
        CREDIT = 3
    }
}