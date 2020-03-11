using HBMarsRover.Common.Exceptions;
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

            if (exception is BadGatewayException)
            {
                result.Error = (exception.Message ?? "Kusura bakma! Şu anda bir sorun yaşıyoruz. Lütfen daha sonra dene.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.BadGateway;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            else if (exception is BadRequestException)
            {
                result.Error = (exception.Message ?? "Yaptığın istekte bir hata var. Lütfen bilgileri kontrol et.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.BadRequest;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is InternalServerErrorException)
            {
                result.Error = (exception.Message ?? "Bir şeyler yanlış gitti. En kısa sürede düzelteceğiz.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (exception is NotFoundException)
            {
                result.Error = (exception.Message ?? "Bulunamadı.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.NotFound;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is RequestTimeoutException)
            {
                result.Error = (exception.Message ?? "İstek zaman aşımına uğradı.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.RequestTimeout;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
            }
            else if (exception is ServiceUnavailableException)
            {
                result.Error = (exception.Message ?? "Hizmet kullanılamıyor.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.ServiceUnavailable;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            }
            else if (exception is ForbiddenException)
            {
                result.Error = (exception.Message ?? "Giriş iznin yok.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.Forbidden;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else if (exception is UnauthorizedException)
            {
                result.Error = (exception.Message ?? "Yetkili değilsin.");
                result.Result = null;
                result.StatusCode = HttpStatusCode.Unauthorized;
                result.Success = false;

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                result.Error = (exception.Message ?? "İşlem sırasında bir hata oluştu.");
                result.Result = null;
                result.StatusCode = (HttpStatusCode)context.Response.StatusCode;
                result.Success = false;
            }

            #endregion            

            Log.Error(exception, exception.Message);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result), Encoding.UTF8);
        }
    }
}
