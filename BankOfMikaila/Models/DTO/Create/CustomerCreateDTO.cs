namespace BankOfMikaila.Models.DTO.Create
{
    public class CustomerCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<AddressDTO> Address { get; set; }
    }
}
