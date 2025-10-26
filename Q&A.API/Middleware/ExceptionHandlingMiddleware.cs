using Q_A.API.Model;

namespace Q_A.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware>logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //_logger.LogError(exception, "An unexpected error occurred.");
            ErrorLog.SaveErrorLog("GlobalSection", exception.ToString());
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync("");
        }
    }
}
