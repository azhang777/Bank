﻿using BankOfMikaila.Models.Enum;

namespace BankOfMikaila.Models.DTO.Create
{
    public class AccountCreateDTO
    {
        public AccountType AccountType { get; set; }
        public string NickName { get; set; }
        public int Rewards { get; set; }
        public double Balance { get; set; }
        public Customer Customer { get; set; }
    }
}