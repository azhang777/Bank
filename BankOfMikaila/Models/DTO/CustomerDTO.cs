﻿namespace BankOfMikaila.Models.DTO
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressDTO> Address { get; set; }
    }
}
