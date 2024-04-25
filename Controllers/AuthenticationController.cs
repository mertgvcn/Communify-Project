using Communify_Backend.Services.Interfaces;
using LethalCompany_Backend.Models.AuthenticationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Communify_Backend.Controllers;

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

    [HttpPost("EmailExists")]
    public async Task<bool> EmailExists([FromBody] EmailExistsRequest request)
    {
        return await authenticationService.EmailExistsAsync(request);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
    {
        var result = await authenticationService.LoginUserAsync(request);

        return result;
    }

    [HttpPost("Register")]
    public async Task Register([FromBody] UserRegisterRequest user)
    {
        await authenticationService.RegisterUserAsync(user);
    }

    [HttpPost("ForgotPassword")]
    public async Task ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await authenticationService.ForgotPasswordAsync(request);
    }

    [HttpPost("SetPassword")]
    public async Task SetPassword([FromBody] SetPasswordRequest request)
    {
        await authenticationService.SetPasswordAsync(request);
    }
}

