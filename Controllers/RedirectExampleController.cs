using Microsoft.AspNetCore.Mvc;

namespace ASPNetLearningCodes.Controllers
{
    [Controller]
    public class RedirectExampleController : Controller
    {
        [Route("bookstore")]
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
            if (bookId <= 0 || bookId > 1000)
            {
                return Content("Invalid Book Id -- Book Id should be between 0 and 1000");
            }
            //return new RedirectToActionResult("NewBookFinder", "RedirectExample", new { }, permanent:false);
            //return RedirectToAction("NewBookFinder", "RedirectExample", new { });  -- both are same

            //return new RedirectToActionResult("NewBookFinder", "RedirectExample", new { }, permanent: true);
            return RedirectToActionPermanent("NewBookFinder", "RedirectExample", new { }); // both are same
        }

        [Route("store/books")]
        public IActionResult NewBookFinder()
        {
            return File("SampleNotes.txt", "text/plain");
        }
    }
}
