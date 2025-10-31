using Microsoft.AspNetCore.Mvc;

namespace ShelfShare.Web.Areas.Admin.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult AddBook()
        {
            return View();
        }
    }
}
