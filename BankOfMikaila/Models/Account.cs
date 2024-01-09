﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text.Json.Serialization;
using BankOfMikaila.Models.Enum;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BankOfMikaila.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        public int Rewards { get; set; }
        [Required]
        public double Balance { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
