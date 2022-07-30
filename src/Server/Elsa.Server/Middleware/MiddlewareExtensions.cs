using Microsoft.AspNetCore.Builder;

namespace Elsa.Server.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlerMiddleware>();
            return builder;

        }

        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<Auth>();
            return builder;
        }
    }
}
