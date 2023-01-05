namespace Nasa.Api.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseUnhandledException(this IApplicationBuilder app) => app.UseMiddleware<UnhandledExceptionMiddleware>();
}
