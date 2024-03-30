using Communify_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Communify_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
        {
            var result = await authenticationService.LoginUserAsync(request);

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register([FromBody] UserRegisterRequest user)
        {
            var result = await authenticationService.RegisterUserAsync(user);

            return result;
        }

        [HttpPost("sendEmail")]
        public async Task SendEmail([FromBody] string toEmail)
        {
            await authenticationService.SendEmail(toEmail);
        }
    }
}
