using Microsoft.AspNetCore.Builder;

namespace HBMarsRover.API.Middlewares
{
    public static class GlobalExceptionExtension
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
