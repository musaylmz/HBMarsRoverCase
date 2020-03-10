using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.API.Middlewares
{
    public class ResponseMetadataMiddleware
    {
        private readonly RequestDelegate _next;
        public ResponseMetadataMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response != null)
            {
                var responseBody = context.Response.Body;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await _next.Invoke(context);

                    context.Response.Body = responseBody;

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var deserializeMemoryStream = JsonConvert.DeserializeObject(new StreamReader(memoryStream).ReadToEnd());

                    string result = JsonConvert.SerializeObject(ResponseMetadataModel.Create((HttpStatusCode)context.Response.StatusCode, deserializeMemoryStream, null));
                    await context.Response.WriteAsync(result, Encoding.UTF8);
                }
            }

        }
    }
}
