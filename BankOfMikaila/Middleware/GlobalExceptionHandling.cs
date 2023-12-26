using BankOfMikaila.Exceptions;
using BankOfMikaila.Response.Format;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankOfMikaila.Middleware
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidTransactionTypeException itte)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = itte.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (InvalidAccountTypeException iate)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = iate.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (InvalidTransactionStatusException itse)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = itse.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (NoFundsAvailableException nfae)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = nfae.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (TransactionNotFoundException tnfe)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = tnfe.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (CustomerNotFoundException cnfe)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = cnfe.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (AccountNotFoundException anfe)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = anfe.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (BillNotFoundException bnfe)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = bnfe.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (CustomException ex)
            {
                ErrorResponse errorResponse = new()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                };

                string jsonResponse = JsonSerializer.Serialize(errorResponse);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
