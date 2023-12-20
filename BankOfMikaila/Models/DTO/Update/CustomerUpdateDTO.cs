namespace BankOfMikaila.Models.DTO.Update
{
    public class CustomerUpdateDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressDTO> Address { get; set; }
    }
}
