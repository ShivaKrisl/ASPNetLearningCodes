namespace ASPNetLearningCodes.Controllers
{
    internal class ControllersMainClass
    {
        internal static void ControllerMainNotes(WebApplicationBuilder builder)
        {
            // A controller is a class that is used to group set of related actions or action methods.
            // An action method is a method of a controller class to perform a specific operation when an HTTP request is received and returns the response.
            // FLOW OF REQUEST IN ASP.NET CORE
            // 1. Request comes to the server
            // 2. Request is passed to the middleware pipeline
            // 3. Middleware pipeline passes the request to the routing middleware
            // 4. Routing middleware finds the suitable controller and action method
            // 5. Action method is executed
            // 6. Response is sent back to the client
            // Example: UserRegistration.cs  is a controller class with an action methods called SignUp and SignIn -- if Request is /UserRegistration/SignUp, SignUp action method will be executed and if Request is /UserRegistration/SignIn, SignIn action method will be executed

            /* Using Controllers
                *1. Create a Folder that can hold all the controllers classes
                *2. Create a User defined Controller class (Your class should have Suffix Controller or add attribute [Controller]) --  A controller class implements Controller class Present in Microsoft.AspNetCore.Mvc.Controller
                *3.Write Your actions methods
                *4. Now to enable Routing for these controller class -- you need to register this controller class into services and enable routing for that action method
                    // Register Class as Controller service -- services.AddControllers();
                    // Enabe Routing for the controller class -- app.UseEndpoints(endpoints => { endpoints.MapControllers(); }); OR app.MapControllers();
                *5. Now just above the action method define the route -- [Route("URL")] -- this will enable routing for the     controller class -- This is called Attribute Routing
                *6.You can also add Multiple Routes to the same action method -- [Route("URL1")] [Route("URL2")] etc but you cannot add same route to multiple action methods
            */
            builder.Services.AddControllers();


            var app = builder.Build();
            app.MapControllers();
            app.Run();

            /* Controller Responsiblities
                1. Reading the Requests => Extracting the data from the request like Query String, Form Data, Route Data, Headers, Cookies, etc
                2. Processing the data => Processing the data like Validating the data, Business Logic, etc
                3. Invoking models => Invoking the models to interact with the database
                4. Preparing the Response => Preparing the response like Rendering the view, Sending the data to the client, etc -- This is called Action Results like ContentResult, JsonResult, ViewResult, IActionResult, etc
            */

            // Action Results
            /* 1. ContentResult -- returns the response of a defined type. Here you need to pass both the RESPONSE(Content) and MIME(Content-type of the response).
             * Common MIMEs are -> text/html, application/json, text/plain, xml, application/pdf, etc
             * 2. JsonResult -- returns the response in JSON format. Here you need to pass the RESPONSE in JSON format.4
                    MIME = application/json
            * 3. FileResult -- returns the content of a file. Here you need to pass the RESPONSE as a file and MIME(Content-type of the file). pdf, image/jpeg, docx, exe, zip, etc  and content-type is the response file type
                We have mainly 3 types of FileResults -- PhysicalFileResult, VirtualFileResult, FileStreamResult
                PhysicalFileResult -- returns the file from the physical path(Outside the project)
                VirtualFileResult -- returns the file from the virtual path(Inside the project) -- Static files should be enabled in the project
                FileStreamResult -- returns the file in form of stream of byte array
             */

            // IActionResult -- is parent of all ActionResult classes -- It is used to return the response of any of ActionResult classes

            // USES OF IActionResult
            // 1. It is used to return the response of any of ActionResult classes  --  if we want to return different types of responses based on the condition then we can use IActionResult

            // Redirection
            /*
             * Url redirection happens when user is redirected to a different URL. Status codes -- 301 and 302
             * 301 -> Permanent Redirection -- This is used when the URL is permanently moved to a new location and browser remembers the new location(cache) so that next time it directly goes to the new location
             * 302 -> Temporary Redirection -- This is used when the URL is temporarily moved to a new location and browser does not remember the new location(cache) here every time request is sent and again redirected
             * Redirection can be done using RedirectResult -- RedirectResult -- returns the response of a defined type. Here you need to pass the URL to which you want to redirect
             * in ActionMethod response headers will have Location header with the new URL
             * 
             *  Scenario: Lets say our URL was /bookstore but over the year we started selling books, mobiles, etc.. so now we need to update as store/books for books, store/mobiles for mobiles, etc but user doesn't know new urls so we need to redirect the user to new urls
             *  1. Request is sent to actionMethod 1 -- /bookstore
             *  2. ActionMethod1 redirects to other actionMethod2 -- /store/books (301, 302)
             *  
             *  This cane be acheived using RedirectToActionResults
             *  RedirectToActionResult("METHODNAME", "CONTROLLERNAME", QueryParamsObj, Permanent:true/false)
             *  "METHODNAME" -- Name of the action method to which you want to redirect eg: "NewBookFinder"
             *  "controllerName" -- Name of the controller class to which you want to redirect eg: if Controller is RedirectExampleController then controllerName is "RedirectExample"
             *  QueryParamsObj -- Query parameters to be passed to the redirected URL if required else we pass empty anonymous object -- new {}
             *  Permanent : true/false -- 301/302
             *  
             *  SHORTCUTS - 
             *  RedirectToActionResult(ACTION,CLASS,QUERY,PERMANENT:false) ---> RedirectToAction(ACTION, CLASS, QUERY)
             *  RedirectToActionResult(ACTION,CLASS,QUERY,PERMANENT:true) ---> RedirectToActionPermanent(ACTION, CLASS, QUERY)
             *  
             */

            /*
             * 1. new LocalRedirectResult("LOCALURL", permanent:TRUE/FALSE) --> works only within your app , cannot redirect to other apps like (google,fb, etc)
             * or use shortcuts -- LocalRedirect()(for 302) && LocalRedirectPermanent() (for 301)
             * 
             * 2. new RedirectResult("URL", permanent:TRUE/FALSE) --> works in redirect to other apps as well
             * use shortcuts -- Redirect("URL"), RedirectPermanent("URL")
             * 
             */

            

        }
    }
}
