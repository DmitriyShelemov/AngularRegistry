using Infrastructure;
using System.Net;
using System.Text.Json;

namespace Web.Infrastructure
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case UserFriendlyException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await response.WriteAsync(BuildErrorResponse(error.Message, response.StatusCode));
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await response.WriteAsync(BuildErrorResponse("Unexpected error occured.", response.StatusCode));
                        break;
                }

                _logger.LogError(error.ToString());
            }
        }

        private static string BuildErrorResponse(string msg, int status)
        {
            return JsonSerializer.Serialize(new { status, errors = new { Message = msg } });
        }
    }
}
