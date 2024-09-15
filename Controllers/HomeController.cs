using Microsoft.AspNetCore.Mvc;
using ASPNetLearningCodes.Controllers.Models;

namespace ASPNetLearningCodes.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
       
        [Route("")]  //  -- Default URL
        public string Index()
        {
            return "This is the default URL";
        }

        [Route("sayhelloto{name:alpha}")]  // This is called Attribute Routing  -- URL will be /sayhello
        [Route("sayhelloagain")]
        public string HelloFromMethod()
        {
            
            return "Hello from Controller Method1";
        }


        [Route("ContentResultDemo")]
        public ContentResult ContentResultDemo()
        {
            // Method1 to create ContentResult 
            /*
            return new ContentResult()
            {
                Content = "This is sample Plain Text",
                ContentType = "text/plain"
            };*/

            // Method2 to create ContentResult -- using Content method from Controller class(Inherited)
            return Content(content:"Sample Plain text using Content Method", contentType:"text/plain");
        }

        [Route("JsonResultDemo")]
        public JsonResult JsonResultDemo()
        {
            PersonData person = new PersonData()
            {
                Id = Guid.NewGuid(),
                Name = "Shiva Krishna",
                Email = "shivakrishnabeeraboina@gmail.com",
                Phone = "1234567890",
                Age = 22
            };
            return Json(person);
        }

        [Route("VirtualFileResult")]
        public VirtualFileResult VirtualFileResultDemo()
        {
            return File("SampleNotes.txt", "text/plain");
        }

        [Route("PhysicalFileResult")]
        public PhysicalFileResult PhysicalFileResultDemo()
        {
            return PhysicalFile("D:/SHIVA KRISHNA/Downloads/Zscalar.pdf", "application/pdf");
        }

        [Route("FileStreamResult")]
        public FileContentResult FileStreamResultDemo()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"D:/SHIVA KRISHNA/Downloads/Zscalar.pdf");
            return File(bytes, "application/pdf");
        }

        // Goal is that in Route query we need to have both isLogged = true and bookid >0 & <1000
        [Route("BookFinder")]
        public IActionResult BookFinder()
        {
            if (!Request.Query.ContainsKey("isLogged"))
            {
                Response.StatusCode = 401;
                return Content("User need to be authenticated to access this page");
            }
            bool isLogged = Convert.ToBoolean(Request.Query["isLogged"]);
            if (!isLogged)
            {
                Response.StatusCode = 401;
                return Content("User need to be authenticated to access this page");
            }
            if (!Request.Query.ContainsKey("bookid"))
            {
                Response.StatusCode = 400;
                return Content("Book Id need to be supplied");
            }
            int bookId = Convert.ToInt32(Request.Query["bookid"]);
            if (bookId <=0 || bookId > 1000)
            {
                return Content("Invalid Book Id -- Book Id should be between 0 and 1000");
            }
            return File("SampleNotes.txt", "text/plain");
        }
    }
}
