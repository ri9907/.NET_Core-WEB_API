//using Entities;
//using Services;

//namespace myProject
//{
//    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
//    public class Middleware
//    {
//        private readonly RequestDelegate _next;
//        //private IRatingService _ratingService;
//        //private readonly ILogger<Middleware> _Ilogger;
//        public Middleware(RequestDelegate next)
//        {
//            _next = next;

//        }

//        public Task Invoke(HttpContext httpContext, IRatingServieces ratingService)
//        {

//            Rating r=new Rating();
//            r.Host = httpContext.Request.Host.Value;
//            r.Method = httpContext.Request.Method;
//            r.Path = httpContext.Request.Path;
//            r.Referer = httpContext.Request.Headers["Referer"].ToString();
//            r.UserAgent = httpContext.Request.Headers["User-Agent"].ToString();
//            r.RecordDate = DateTime.UtcNow;
//            ratingService.Add(r);
//            return _next(httpContext);
//        }
//    }

//    // Extension method used to add the middleware to the HTTP request pipeline.
//    public static class MiddlewareExtensions
//    {
//        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<Middleware>();
//        }
//    }
//}

using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services;
using System.Threading.Tasks;

namespace myProject
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IRatingServieces ratingService)
        {
            Rating rating = new Rating();
            rating.Host = httpContext.Request.Host.Value;
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path;
            rating.Referer = httpContext.Request.Headers["Referer"].ToString();
            rating.UserAgent = httpContext.Request.Headers["User-Agent"].ToString();
            rating.RecordDate = new DateTime();

  


            ratingService.Add(rating);

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}

