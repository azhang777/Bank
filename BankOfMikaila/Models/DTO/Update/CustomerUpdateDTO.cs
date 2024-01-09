namespace BankOfMikaila.Models.DTO.Update
{
    public class CustomerUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<AddressDTO> Address { get; set; }
    }
}
