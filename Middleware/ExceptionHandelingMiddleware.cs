namespace learn_asp_clean_structure.Middleware;

using System.Net;
using System.Text.Json;

public class ExceptionHandelingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandelingMiddleware> _logger;

    public ExceptionHandelingMiddleware(RequestDelegate next, ILogger<ExceptionHandelingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandeled exception occured");
            await HandleExceptionAsync(context, ex);
        }   
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal server error",
            Detail = ex.Message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}