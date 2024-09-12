using ASPNetLearningCodes.MiddleWares;

namespace ASPNetLearningCodes.Routing
{
    internal class RoutingMain
    {
        internal static void MainClassNotes(WebApplication app)
        {
            // is process of matching the incoming request with the urls and methods and invoke corresponding end point (middleware)
            // To acheive routing 1. we need to add the routing middleware -- app.UseRouting();
            // 2. Then we need to write EndPoints -- app.UseEndpoints(ep => { <METHOD&URL>, <MIDDLEWARE> });
            // UseEndpoints will actually execute the endpoint; UseRouting will only do the matching
            app.UseRouting();


            
            app.UseEndpoints(endPoints =>
            {
                endPoints.Map("/home", async (httpContext) =>
                {
                    await httpContext.Response.WriteAsync("Base Url");
                });

                endPoints.Map("/about", async (httpContext) =>
                {
                    await httpContext.Response.WriteAsync("Home Url");
                });

            });
            

            // Use custom middleware in Route
            
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("/home", async (context) =>
                {
                    app.UseMiddleware<CustomConventionalMiddleWare>();
                });
            });
            

            // Endpoints
            // 1. Map -- used to map the url to the middleware irresective of the http method (checks only url)
            // 2. MapGet -- used to map the url to the middleware only for GET method
            // 3. MapPost -- used to map the url to the middleware only for POST method
            // 4. MapPut -- used to map the url to the middleware only for PUT method
            // 5. MapDelete -- used to map the url to the middleware only for DELETE method
            // 6. MapControllers -- used to map the url to the respective controller
            // 7. MapRazorPages -- used to map the url to the respective razor page
            // 8. MapBlazorHub -- used to map the url to the respective blazor hub
            // 9. MapHub -- used to map the url to the respective hub
            // 10. MapFallback --map the url to respective middleware when no other middleware is matched
            // 11. MapFallbackToFile --map url to the respective file when no other middleware is matched
            // 12. MapFallbackToPage --map the url to respective page when no other middleware is matched
            // 13. MapHealthChecks -- used to map the url to the respective health check middleware
            // 14. MapWhen -- used to map the url to the respective middleware when the predicate is true
            // 15. MapAreaControllerRoute -- used to map the url to the respective area controller
            // 16. MapAreaPageRoute -- used to map the url to the respective area page

             //Using Get and Post Methods
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("/home", async context =>
                {
                    await context.Response.WriteAsync("This is Home Page Called only Using Get Request");
                });
                endPoints.MapPost("/register", async context =>
                {
                    await context.Response.WriteAsync("This is Register Page called only using Post Request");
                });
            });
            

            // Route Parameters
            // are parameters that come from the url - they are dynamic; dont give spaces

            // 1. default Parameters --  if values is not defined, it will take the default value as defined
            
            app.UseEndpoints(endPoints =>
            {
                // here if value is not defined, it will take the default value as Shiva
                endPoints.MapGet("/profiles/{username=Shiva}", async context =>
                {
                    // Return type of RouteValues is object -- so we need to convert it to string
                    string? name = Convert.ToString(context.Request.RouteValues["username"]);
                    await context.Response.WriteAsync($"This is Profile Page of {name}");
                });

                // products/id/{id} -- default value is 1
                endPoints.MapGet("products/id/{id=1}", async context =>
                {
                    int id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    await context.Response.WriteAsync($"This is Product Page of {id}");
                } );
            });
            

            // 2. Optional Parameters  --  if values is not defined, it will take the default value as null
            
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("products/id/{id?}", async context =>
                {
                    // with this you can check if the id is present or not and can show the respective page
                    if (context.Request.RouteValues.ContainsKey("id"))
                    {
                        int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
                        await context.Response.WriteAsync($"This is Product Page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Product Page of All Products");
                    }
                });
            });
            

            // Routing Constraints - defines constraints on the route parameters so that the route will be matched only if the constraints are satisfied
            // 1. Type Constraints -  {parameter:type} ie, {id:int} -- id should be of integer only
            // 2. Regular Expression Constraints - {parameter:regex} ie, {id:regex(\\d{3})} -- id should be of 3 digits only
            // 3. Length Constraints - {parameter:length(min,max)} ie, {id:length(3,5)} -- id should be of 3 to 5 digits only
            // 4. Min and Max Constraints - {parameter:min(min)} and {parameter:max(max)} ie, {id:min(100)} -- id should be minimum of 100 characters
            // 5. Range Constraints - {parameter:range(min,max)} ie, {id:range(100,200)} -- id should be between 100 and 200
            // 6. Required Constraints - {parameter:required} ie, {id:required} -- id should be present in the url

            // -- You can write multiple constraints on the same parameter by separating them with a : ie, {id:int:min(100):max(200)}

            // 1. Type Constraints  
            // int, bool(true,false,TRUE, FALSE), alpha (lower case & uppercase), decimal, double, datetime(yyyy-mm-dd-hh-mm-ss-tt OR mm-dd-yyyy-hh-mm-ss-tt), float, guid, long
            
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("products/id/{id:int?}", async context =>
                {
                    int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (id.HasValue)
                    {
                        await context.Response.WriteAsync($"This is Product Page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Product Page of All Products {id}");
                    }
                });
                endPoints.MapGet("employee/profiles/{name:alpha?}", async context =>
                {
                    string? name = Convert.ToString(context.Request.RouteValues["name"]);
                    if (name.Length>0)
                    {
                        await context.Response.WriteAsync($"This is Profile Page of {name}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Profile Page of All Employees");
                    }
                });
                endPoints.MapGet("daily-attendance-report/{date:datetime}", async context =>
                {
                   DateTime? time = Convert.ToDateTime(context.Request.RouteValues["date"]);
                    if (time.HasValue)
                    {
                        await context.Response.WriteAsync($"This is the attendance Report of {time}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"Incorrect null date supplied - {time}");
                    }
                });

                endPoints.MapGet("login/id/{id:guid}", async context =>
                {
                    Guid? id = Guid.Parse(Convert.ToString(context.Request.RouteValues["id"])!);
                    if (id.HasValue)
                    {
                        await context.Response.WriteAsync($"This is Login Page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"Incorrect ID {id}");
                    }
                });
            });
            

            // 2. Length Constraints
            // minlength, maxlength, length  -- applicable for string type only
            
            app.UseEndpoints(endPoints =>
            {
                // minimum len(chars) = 3 && maximum len(chars) = 7
                endPoints.MapGet("profile/{name:minlength(3):maxlength(7)}", async context =>
                {
                    string? name = Convert.ToString(context.Request.RouteValues["name"]);
                    await context.Response.WriteAsync($"This is Profile Page of {name}");
                });

                // Total len of chars should be atmost 15
                endPoints.MapGet("profile/{name:length(15)}", async context =>
                {
                    string? name = Convert.ToString(context.Request.RouteValues["name"]);
                    if (name.Length > 0)
                    {
                        await context.Response.WriteAsync($"This is profile page of {name}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Invalid Name");
                    }
                });

                // Total len of chars should be between min 24 and max 27
                endPoints.MapGet("profile/{name:length(24,27)}", async context =>
                {
                    string? name = Convert.ToString(context.Request.RouteValues["name"]);
                    await context.Response.WriteAsync($"This is Profile Page of {name}");
                });
            });
            

            // 3. Value Constraints
            // min, max, range -- applicable for int, double, float, decimal, long, datetime, guid
            
            app.UseEndpoints(endPoints =>
            {
                // min 3 ,max inf
                endPoints.MapGet("products/id/{id:int:min(3)}", async context =>
                {
                int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (id.HasValue)
                    {
                        await context.Response.WriteAsync($"This is Product Page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Product Page of All Products {id}");
                    }
                });

                // max only 5
                endPoints.MapGet("products/id/{id:int:max(5)}", async context =>
                {
                    int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (id.HasValue)
                    {
                        await context.Response.WriteAsync($"This is product page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Product Page of All Products {id}");
                    }
                });

                // values between 3 and 10
                endPoints.MapGet("products/id/{id:int:range(3,10)}", async context =>
                {
                    int? id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (id.HasValue)
                    {
                        await context.Response.WriteAsync($"This is product page of {id}");
                    }
                    else
                    {
                        await context.Response.WriteAsync($"This is Product Page of All Products {id}");
                    }
                });

            });
            

            // 4. Regular Expression Constraints
            // regex -- applicable for string type only
            
            app.UseEndpoints(endPoints =>
            {
                // like sales-report/2021/jan  -- here year should be min 2000 and month should be in jan,feb,mar,apr only
                endPoints.MapGet("sales-report/{year:int:min(2000)}/{month:regex(^(jan|feb|mar|apr)$)}",async context =>
                {
                    int? year = Convert.ToInt32(context.Request.RouteValues["year"]);
                    string? month = Convert.ToString(context.Request.RouteValues["month"]);
                    await context.Response.WriteAsync($"This is Sales Report of {month} {year}");
                } );
            });
            

            // 5. Required Constraints
            // required -- applicable for all types

            /* 6. Custom Constraints - These are user defined constraints that are defined in the application and can be used in the routing.
             * This can acheived by using a custom class that implements IRouteConstraint interface
             * 1. Create a class that implements IRouteConstraint interface (which has a method Match)
             * 2. The Match method has parameters of HttpContext, IRouter, RouteKey, RouteValueDictionary,      RouteDirection  and returns a boolean value
             *      *HttpContext context -> current http context
             *      *IRouter route -> current router object
             *      *RouteKey(string) -> route parameter on which the custom class constraint is applied
             *      *RouteValueDictionary values  -> represents the route values(one or more) of that                    RouteKey
             *      *RouteDirection -> is an enum that represents the direction of the route
             *          it can be IncomingRequest(check whether constraint matches with incoming request ot          not) or UrlGeneration(Generate a url based on the constraint(in MVC's or generating          hyperlinks in the views)).
             * 3. In the Match method write the logic to check whether the constraint is satisfied or not
             * 4. Register the custom constraint in the services collection using code 
             *      builder.Services.AddRouting(options => {
             *          options.ConstraintMap.Add("NAMEOFCONTSRAINT", typeof(THATCUSTOMCLASSNAME));
             *      });
             * 5. Now this custom constraint can be used in the routing like:
             *      {id:int:NAMEOFCONSTRAINT} -- similar to pre-defined like {id:int:length(5)} 
             */
            
            app.UseEndpoints(endPoints =>
            {
                // here we added monthCheck (name that we defined in builder.Services.AddRouting) as custom constraint
                endPoints.MapGet("attendance/{year:int:min(2017)}/{month:monthCheck}", async context =>
                {
                    int? year = Convert.ToInt32(context.Request.RouteValues["year"]);
                    string? month = Convert.ToString(context.Request.RouteValues["month"]);
                    await context.Response.WriteAsync($"This is Attendance Report of {month} {year}");
                });
            });
            

            // End Point Selection -- EndPoints are not selected based on the order they defined. If url matches with more than one endpoint then it follow precedency rules.
            // RULE 1 : URL with more segments is executed first. ie, a/b/c/d  >> a/b/c
            // RULE 2: URL that has literal text is higher than URL with parameter text. ie, a/b >> a/{bValue:int}
            // RULE 3: (For Parameter Literals)URL segment that has constraints are higher than URL with no constraints.            ie, a/{b:int?} >> a/{b}
            // RULE 4: (catch Parameters (**))URL segment with specific parameters has higher order than any match URL(**). ie, a/{b} >> a/**

            
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapGet("sales-report/{year:int}/{month:alpha}", async context =>
                {
                    int? id = Convert.ToInt32(context.Request.RouteValues["year"]);
                    string? month = Convert.ToString(context.Request.RouteValues["month"]);
                    if (id.HasValue && month.Length > 0)
                    {
                        await context.Response.WriteAsync($"This is Sales Report of year : {id} and Month: {month}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("No valid Year or Month supplied");
                    }
                });

                endPoints.MapGet("sales-report/2024/jan", async context =>
                {
                    await context.Response.WriteAsync("This is the sales report of 2024, Jan");
                });
            });
            

            // WEB ROOT -- 
            /*
                * Our application may also contain static files like images, css, js, etc. These files are stored in the    wwwroot folder inside the application instead of storing and retrieving from the database. This is called Web Root.
                *To serve the static files, we need to add the middleware app.UseStaticFiles(); -- this will serve the files from the wwwroot folder
                * wwwroot is the default folder where static files are stored. If you want to change the folder, you can do it by using the UseStaticFiles method like app.UseStaticFiles(new StaticFileOptions() { FileProvider = new PhysicalFileProvider("path") });  OR
                * by configuring WebApplication Builder options from args to new Path - like:
                * var builder = WebApplicationBuilder.CreateBuilder(new WebApplicationOptions(){
                *   WebRootPath = "FOLDERNAME"
                * });
                * You can also have multiple wwwroot folders
                * 
                * BUT WE NEED TO HAVE wwwroot folder even if we supply our custom folder
                
            */
            // Statoc Files can be accessed like : appurl/filename.extension
            app.UseEndpoints(endPoints =>
            {
                // app url - localhost:5000
                endPoints.MapGet("/",async context =>
                {
                    await context.Response.WriteAsync("This is the Home Page");
                });
            });

        }
    }
}
