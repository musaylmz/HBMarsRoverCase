using Microsoft.AspNetCore.Builder;

namespace HBMarsRover.API.Middlewares
{
    public static class ResponseMetadataExtension
    {
        public static IApplicationBuilder UseResponseMetadataMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMetadataMiddleware>();
        }
    }
}
