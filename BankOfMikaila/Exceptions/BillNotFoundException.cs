namespace BankOfMikaila.Exceptions
{
    public class BillNotFoundException : CustomException
    {
        public BillNotFoundException(string message) : base(message)
        {
        }
    }
}
