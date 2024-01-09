namespace BankOfMikaila.Exceptions
{
    public class TransactionNotFoundException : CustomException
    {
        public TransactionNotFoundException(string message) : base(message)
        {
        }
    }
}
