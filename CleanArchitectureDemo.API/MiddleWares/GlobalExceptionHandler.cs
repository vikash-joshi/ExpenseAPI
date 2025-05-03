using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    //private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
       // _logger = logger;
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
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        //_logger.LogError(exception, "An unhandled exception occurred.");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error. Please try again later."
        });

        return context.Response.WriteAsync(result);
    }
}