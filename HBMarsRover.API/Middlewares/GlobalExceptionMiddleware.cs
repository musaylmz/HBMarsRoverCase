using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HBMarsRover.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// The log
        /// </summary>
        private readonly Serilog.ILogger Log = Serilog.Log.ForContext<ResponseMetadataMiddleware>();
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <exception cref="ArgumentNullException">next</exception>
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response != null)
            {
                var responseBody = context.Response.Body;
                try
                {
                    await _next.Invoke(context);
                }
                catch (Exception ex)
                {
                    context.Response.Body = responseBody;
                    await HandleExceptionAsync(context, ex);
                }
            }
        }
        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new ResponseMetadataModel();

            context.Response.ContentType = "application/json";

            #region Exception

            result.Error = (exception.Message ?? "İşlem sırasında bir hata oluştu.");
            result.Result = null;
            result.StatusCode = (HttpStatusCode)context.Response.StatusCode;
            result.Success = false;

            #endregion            

            Log.Error(exception, exception.Message);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result), Encoding.UTF8);
        }
    }
}
