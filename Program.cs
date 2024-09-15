using ASPNetLearningCodes.MiddleWares;
using ASPNetLearningCodes.MiddleWares.EmailAndPasswordCustom;
using ASPNetLearningCodes;
using Microsoft.Extensions.FileProviders;
using System.Text;
using ASPNetLearningCodes.Routing;
using Microsoft.Extensions.Primitives;
using ASPNetLearningCodes.Controllers;


//var builder = WebApplication.CreateBuilder(args);

// For Static Folder Configuration
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "StaticFolder"
});


/*
builder.Services.AddTransient<CustomMiddleWare>();
builder.Services.AddSingleton<HomeController>();

// Adding Custom Routing Constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("monthCheck",typeof(MonthsCustomConstraint));
});

// be
var app = builder.Build();

// For serving static files like html, css, js, images etc
app.UseStaticFiles(); // This is for our StaticFolder that we have configured in Builder

app.UseStaticFiles(new StaticFileOptions(){
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath , "\\StaticFolder2")),  // This is for Our Other Static Folder 
});


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



CalculatorOperationsRequests.Caluclator(app);



// Middlewares execute in the order that you define
// Use is to add middlewares and call next ; can also used as terminal middleware
// Run is terminal or short circuiting middleware

// Middleware one
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("First Middleware - Start \n");
    await next(context);
    await context.Response.WriteAsync("First Middleware - End \n");
});

// Middleware two
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Second Middleware - Start \n");
    await next(context);
    await context.Response.WriteAsync("Second Middleware - End \n");
});

// Custom middleware  - if the amount of code you write in middleware is huge or that middleware is used more than once it is better to write your middleware as seperate class
// 1. First add your class as Service in your builder  -- builder.Services.AddTransient<CustomMiddleWare>();
// 2. Your class should implement IMiddleware interface that has method InvokeAsync
// 3. Use that method to write your code, call the next middleware in the pipeline and then write your code
// 4. In the main program use it as app.UseMiddleware<CustomMiddleWare>();
app.UseMiddleware<CustomMiddleWare>();


// Custom Conventional Middleware
// 1. Create a class that has a method Invoke that takes HttpContext as parameter and RequestDelegate as parameter
// 2. In the Invoke method write your code, call the next middleware in the pipeline and then write your code
// 3. Create an extension method that takes IApplicationBuilder as parameter and returns IApplicationBuilder
// 4. In the extension method use UseMiddleware<YourClassName> to add your middleware to the pipeline
// 5. In the main program use it as app.UseCustomConventionalMiddleWare();
app.UseCustomConventionalMiddleWare();


// UseWhen -- use middle ware when only when some conditions are true 
// Syntax: app.UseWhen(Predicate, Action) -- Predicate is a delegate that takes HttpContext as parameter and returns boolean value
// Action is a delegate that takes HttpContext and RequestDelegate as parameters -- which executes a middleware
// Both should be lambda expressions
app.UseWhen( context => context.Request.Query.ContainsKey("firstname"),
app =>
{
    app.Use(async (HttpContext httpcontext, RequestDelegate next) =>
    {
        await httpcontext.Response.WriteAsync("This middleware will be executed only when firstname is present in the query string \n");
        await next(httpcontext);
    });
});

// Middleware three  -- Terminating middleware
app.Run(async(HttpContext httpContext) =>
{
    await httpContext.Response.WriteAsync("Last middleware \n");
});


/// CORRECT ORDER OF MIDDLEWARE  -- these to be defined in the startup.cs file
/// 1. ExceptionalHandler Middleware
/// 2. HSTS - Http Strict Transportation Security Middleware
/// 3. HTTPS Redirection Middleware
/// 4. Static Files Middleware
/// 5. Routing Middleware
/// 6. CORS Middleware
/// 7. Authentication Middleware
/// 8. Authorization Middleware
/// 9. Session Middleware
/// 10. MapControllers Middleware
/// 11. Custom Middlewares
/// 12. Endpoints Middleware



// Assignment  -- Check email and password in query string and return success or failure when request is post
app.UseWhen(context => context.Request.Method == "POST",
    app =>
    {
        app.UseCustomMiddleWareForEmailAndPassword();
    }
);

app.UseWhen(context => context.Request.Method == "GET",
    app =>
    {
        app.Use(async (httpContext, next) =>
        {
            httpContext.Response.StatusCode = 200;
            await httpContext.Response.WriteAsync("Hello World!");
            await next(httpContext);
        });
    }
);

// Routing
RoutingMain.MainClassNotes(app);


app.Run();
*/ 

ControllersMainClass.ControllerMainNotes(builder);
