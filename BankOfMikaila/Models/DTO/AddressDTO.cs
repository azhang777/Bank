namespace BankOfMikaila.Models.DTO
{
    public class AddressDTO
    {
        public long Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; } 
    }
}
