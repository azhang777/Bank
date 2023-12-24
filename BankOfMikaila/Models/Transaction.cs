﻿using BankOfMikaila.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankOfMikaila.Models
{
    public class Transaction
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required]
        public TransactionStatus TransactionStatus { get; set; }
        [Required]
        public TransactionMedium TransactionMedium { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("OwnerId")]
        public long OwnerId { get; set; }
        public virtual Account Owner { get; set; }

        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }
    }
}
