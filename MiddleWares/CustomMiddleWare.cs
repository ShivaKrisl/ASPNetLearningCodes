namespace ASPNetLearningCodes.MiddleWares
{
    public class CustomMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next){
            await context.Response.WriteAsync("Custom Middleware - Start \n");
            await next(context);  // This will call the next middleware in the pipeline
            await context.Response.WriteAsync("Custom Middleware - End \n");
        }

    }
}
