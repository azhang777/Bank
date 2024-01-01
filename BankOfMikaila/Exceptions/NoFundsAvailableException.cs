namespace BankOfMikaila.Exceptions
{
    public class NoFundsAvailableException : CustomException
    {
        public NoFundsAvailableException(string message) : base(message)
        {
        }
    }
}
