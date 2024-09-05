using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext httpContext) =>
{
    //httpContext.Response.StatusCode = 200;

    httpContext.Response.Headers["Content-type"] = "text/html";
    httpContext.Response.Headers["X-Powered-By"] = "ASP.NET Core";
    await httpContext.Response.WriteAsync("Hello World!");
    await httpContext.Response.WriteAsync("<h1> Keerthika Cherala </h1>");

    // http Request Methods

    // Get path
    string path = httpContext.Request.Path;
    await httpContext.Response.WriteAsync($"<h1> Path: {path}</h1>");

    // Get Request Method
    string method = httpContext.Request.Method;
    await httpContext.Response.WriteAsync($"<h1> Method: {method}</h1>");

    // Get Response StatusCode
    int statusCode = httpContext.Response.StatusCode;
    await httpContext.Response.WriteAsync($"<h3>Status Code = {statusCode}</h3>");

    // Get Request Headers
    IHeaderDictionary headers = httpContext.Request.Headers;
    foreach (var kvp in headers)
    {
        var key = kvp.Key;
        var value = kvp.Value;
        await httpContext.Response.WriteAsync($"<p>{key} : {value}</p>");
    }

    Stream requestBody = httpContext.Request.Body;
    using (StreamReader reader = new StreamReader(requestBody, Encoding.UTF8))
    {
        string body = await reader.ReadToEndAsync();
        await httpContext.Response.WriteAsync($"<p>Request Body: {body}</p>");
    }


});

//app.MapGet("/", () => "Hello World!");

app.Run();
