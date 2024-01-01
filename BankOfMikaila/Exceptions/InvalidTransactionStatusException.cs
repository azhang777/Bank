namespace BankOfMikaila.Exceptions
{
    public class InvalidTransactionStatusException : CustomException
    {
        public InvalidTransactionStatusException(string message) : base(message)
        {
        }
    }
}
