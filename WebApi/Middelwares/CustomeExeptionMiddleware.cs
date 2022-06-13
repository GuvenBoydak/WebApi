
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebApi.Services;

namespace WebApi.Middelwares
{
    public class CustomeExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;


        public CustomeExeptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = Stopwatch.StartNew();
            try
            {

                string message = "[Request] " + "   HTTP " + context.Request.Method + " - " + context.Request.Path;
                _logger.Write(message);

                await _next(context);
                watch.Stop();

                message = "[Responce] " + " HTTP " + context.Request.Method + " - " + context.Request.Path + " Responced " + context.Response.StatusCode + " in " + watch.Elapsed.Milliseconds;
                _logger.Write(message);

            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExeption(context, ex, watch);
            };


        }

        private Task HandleExeption(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]   HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.Milliseconds;
            _logger.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);


        }
    }

    public static class CustomeExeptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomeExeption(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomeExeptionMiddleware>();
        }

    }
}
