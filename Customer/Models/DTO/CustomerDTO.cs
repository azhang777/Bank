namespace Customer.Models.DTO
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<AddressDTO> Address { get; set; }
    }
}
