namespace BankOfMikaila.Exceptions
{
    public class InvalidTransactionTypeException : CustomException
    {
        public InvalidTransactionTypeException(string message) : base(message)
        {
        }
    }
}
