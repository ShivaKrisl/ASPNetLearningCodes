using ASPNetLearningCodes;
using Microsoft.Extensions.Primitives;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
app.Run(async (HttpContext httpContext) =>
{

    //httpContext.Response.StatusCode = 200;

    httpContext.Response.Headers["Content-type"] = "text/html";
    httpContext.Response.Headers["X-Powered-By"] = "ASP.NET Core";
    await httpContext.Response.WriteAsync("Hello World!");
    await httpContext.Response.WriteAsync("<h1> Shiva Krishna Beeraboina </h1>");

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

    // Get Route Values
    RouteValueDictionary routeValues = httpContext.Request.RouteValues;
    foreach (var kvp in routeValues)
    {
        var key = kvp.Key;
        var value = kvp.Value;
        await httpContext.Response.WriteAsync($"<p>{key} : {value}</p>");
    }

    // Query String - GET Request
    if (httpContext.Request.Method == "GET")
    {
        if (httpContext.Request.Query.ContainsKey("id"))
        {
            string id = httpContext.Request.Query["id"];
            await httpContext.Response.WriteAsync($"<p>Id: {id}</p>");
        }
    }

    if (httpContext.Request.Method == "POST")
    {
        StreamReader reader = new StreamReader(httpContext.Request.Body);
        string body = await reader.ReadToEndAsync();

        // Read Query Parameters --  You will get request in string ; need to convert to key-value pairs
        // There might be chance that there can be multiple values for same key like age=20&age=30 so for that we use StringValues data type
        Dictionary<string, StringValues> queryDict =  Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery( body );
        foreach (string key in queryDict.Keys)
        {
            StringValues values = queryDict[key];
            await httpContext.Response.WriteAsync($"<p>{values.ToString()}</p>");
        }

        if (queryDict.ContainsKey("firstName"))
        {
            StringValues firstNames = queryDict["firstName"];
            // get only first value of first name
            string firstValueOfFirstName = firstNames[0];
            await httpContext.Response.WriteAsync($"<p>First Value of First Name: {firstValueOfFirstName}</p>");

            // get all values of first name
            foreach (string firstName in firstNames)
            {
                await httpContext.Response.WriteAsync($"<p>First Name: {firstName}</p>");
            }
        }



    }

});
*/
//app.MapGet("/", () => "Hello World!");

CalculatorOperationsRequests.Caluclator(app);

app.Run();
