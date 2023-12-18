﻿using BankOfMikaila.Models.Enum;

namespace BankOfMikaila.Models.DTO
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public TransactionMedium TransactionMedium { get; set; }
        public double Amount { get; set; }
        public long Account1_Id { get; set; }
        public long Account2_Id { get; set; }
    }
}