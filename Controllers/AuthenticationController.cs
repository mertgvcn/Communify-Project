using Microsoft.AspNetCore.Mvc;

namespace Communify_Backend.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
