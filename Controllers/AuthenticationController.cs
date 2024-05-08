﻿using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Communify_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("EmailExists")]
    [AllowAnonymous]
    public async Task<bool> EmailExists([FromBody] EmailExistsRequest request)
    {
        return await authenticationService.EmailExistsAsync(request);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
    {
        var result = await authenticationService.LoginUserAsync(request);

        return result;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task Register([FromBody] UserRegisterRequest user)
    {
        await authenticationService.RegisterUserAsync(user);
    }

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await authenticationService.ForgotPasswordAsync(request);
    }

    [HttpPost("ChangePassword")]
    [Authorize(Roles = "User")]
    public async Task ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await authenticationService.ChangePasswordAsync(request);
    }

    [HttpPost("SetPassword")]
    [AllowAnonymous]
    public async Task SetPassword([FromBody] SetPasswordRequest request)
    {
        await authenticationService.SetPasswordAsync(request);
    }

}

