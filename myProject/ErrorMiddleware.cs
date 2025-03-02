using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyFirstWebApiSite
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ErrorMiddleware> Ilogger)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
             Ilogger.LogInformation($"my error{ex.Message} from middelware ......");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("internal error in server");
            }
        
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
