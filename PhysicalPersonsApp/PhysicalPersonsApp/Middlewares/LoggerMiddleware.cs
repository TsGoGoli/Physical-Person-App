namespace PhyisicalPersonsApp.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
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
            _logger.LogError(ex, "An unexpected error occurred while processing the request.");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var errorResponse = new { message = "An internal server error." };
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}