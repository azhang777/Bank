using System.Net;

namespace BankOfMikaila.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }

        /*
         * CustomerNotFoundException
         * InvalidAccountTypeException
         * AccountNotFoundException
         * TransactionNotFoundException
         * InvalidTransactionStatusException
         * BillNotFoundException
         */ 
    }
}
