using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Communify_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        [HttpGet("GetString")]
        public string GetString()
        {
            return "selam";
        }
    }
}
