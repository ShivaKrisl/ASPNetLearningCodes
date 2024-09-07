using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ASPNetLearningCodes.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomConventionalMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomConventionalMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await httpContext.Response.WriteAsync("Custom Conventional Middleware - Start \n");

            if (httpContext.Request.Query.ContainsKey("firstname") && httpContext.Request.Query.ContainsKey("lastname"))
            {
                string? firstName = httpContext.Request.Query["firstname"];
                string? lastName = httpContext.Request.Query["lastname"];

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                {
                    await httpContext.Response.WriteAsync($"First Name: {firstName}  ");
                    await httpContext.Response.WriteAsync($"Last Name: {lastName}\n");
                }
                else
                {
                    await httpContext.Response.WriteAsync($"First Name or Last Name is missing \n");
                }  
            }
            await _next(httpContext);

            await httpContext.Response.WriteAsync("Custom Conventional Middleware - End \n");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomConventionalMiddleWareExtensions
    {
        public static IApplicationBuilder UseCustomConventionalMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomConventionalMiddleWare>();
        }
    }
}
