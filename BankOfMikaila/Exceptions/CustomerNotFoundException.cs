namespace BankOfMikaila.Exceptions
{
    public class CustomerNotFoundException : CustomException
    {
        public CustomerNotFoundException(string message) : base(message)
        {
        }
    }
}
