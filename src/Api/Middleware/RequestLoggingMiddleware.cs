namespace WhatsReal.Api.Middleware;

/// <summary>
/// Middleware for logging HTTP requests and responses.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var requestId = context.TraceIdentifier;

        _logger.LogInformation(
            "HTTP {Method} {Path} started. RequestId: {RequestId}",
            context.Request.Method,
            context.Request.Path,
            requestId);

        var originalBodyStream = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                var duration = DateTime.UtcNow - startTime;
                _logger.LogInformation(
                    "HTTP {Method} {Path} completed with {StatusCode} in {Duration}ms. RequestId: {RequestId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    duration.TotalMilliseconds,
                    requestId);
            }
            finally
            {
                responseBody.Position = 0;
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
