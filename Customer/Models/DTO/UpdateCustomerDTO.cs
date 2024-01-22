namespace Customer.Models.DTO
{
    public class UpdateCustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<AddressDTO> Address { get; set; }
    }
}
    