namespace BankOfMikaila.Exceptions
{
    public class AccountNotFoundException : CustomException
    {
        public AccountNotFoundException(string message) : base(message)
        {
        }
    }
}
