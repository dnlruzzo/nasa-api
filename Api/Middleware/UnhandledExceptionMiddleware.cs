using FluentValidation;
using System.Net.Mime;

namespace Nasa.Api.Middleware;

public class UnhandledExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public UnhandledExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;

            context.Response.StatusCode = error switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                NotImplementedException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            if (error is ValidationException validationException)
            {
                await context.Response.WriteAsJsonAsync(validationException.Errors);
            }
            else
            {
                await context.Response.WriteAsJsonAsync(error.Message);
            }
        }
    }
}
