using Microsoft.AspNetCore.Mvc;

namespace ShelfShare.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
