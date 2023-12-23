﻿using BankOfMikaila.Models.Enum;

namespace BankOfMikaila.Models.DTO.Create
{
    public class TransactionCreateDTO
    {
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionMedium TransactionMedium { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
