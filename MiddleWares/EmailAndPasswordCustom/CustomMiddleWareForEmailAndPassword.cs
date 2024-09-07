using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ASPNetLearningCodes.MiddleWares.EmailAndPasswordCustom
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleWareForEmailAndPassword
    {
        private readonly RequestDelegate _next;

        public CustomMiddleWareForEmailAndPassword(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("email")==false && httpContext.Request.Query.ContainsKey("password") == false){
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for 'email'\r\nInvalid input for 'password'");
                await _next(httpContext);
                return;
            }
            if (httpContext.Request.Query.ContainsKey("email"))
            {
                string? email = httpContext.Request.Query["email"];
                if (string.IsNullOrEmpty(email) || email != "admim@example.com")
                {
                    httpContext.Response.StatusCode = 400;
                    // Invalid Login
                    await httpContext.Response.WriteAsync("Invalid Login");
                    await _next(httpContext);
                }
                else
                {
                    if (httpContext.Request.Query.ContainsKey("password"))
                    {
                        string? password = httpContext.Request.Query["password"];
                        if (string.IsNullOrEmpty(password) || password != "admin1234")
                        {
                            httpContext.Response.StatusCode = 400;
                            // Invalid Login
                            await httpContext.Response.WriteAsync("Invalid Login");
                            await _next(httpContext);
                        }
                        else
                        {
                            httpContext.Response.StatusCode = 200;
                            await httpContext.Response.WriteAsync("Login Successfull"); 
                            await _next(httpContext);
                        }
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid input for 'Password'");
                        await _next(httpContext);
                    }
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for 'Email'");
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddleWareForEmailAndPasswordExtensions
    {
        public static IApplicationBuilder UseCustomMiddleWareForEmailAndPassword(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleWareForEmailAndPassword>();
        }
    }
}
