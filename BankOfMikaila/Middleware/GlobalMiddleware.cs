using BankOfMikaila.Exceptions;
using BankOfMikaila.Response.Format;
using Serilog;
using System.Diagnostics;
using System.Text.Json;

namespace BankOfMikaila.Middleware
{
    public class GlobalMiddleware //exception handling and logger middleware
    {
        private readonly RequestDelegate _next;

        public GlobalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
                Log.Information(
                    "Request {RequestMethod} {RequestPath} processed in {Elapsed:0.0000} ms with status code {StatusCode}",
                    context.Request.Method, context.Request.Path, stopwatch.Elapsed.TotalMilliseconds, context.Response.StatusCode);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    itte.GetType().Name, itte.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    iate.GetType().Name, iate.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    itse.GetType().Name, itse.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    nfae.GetType().Name, nfae.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    tnfe.GetType().Name, tnfe.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    cnfe.GetType().Name , cnfe.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                     anfe.GetType().Name, anfe.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    bnfe.GetType().Name, bnfe.Message, context.Request.Method);
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
                Log.Error(
                    "An error occurred while processing the request: {Exception} with message {Message} at {RequestMethod}",
                    ex.GetType().Name, ex.Message, context.Request.Method);
            }
            finally
            {
                stopwatch.Stop();
            }
        }
    }
}
