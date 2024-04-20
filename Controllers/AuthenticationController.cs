﻿using Communify_Backend.Services.Interfaces;
using LethalCompany_Backend.Models.AuthenticationModels;
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

    [AllowAnonymous]
    [HttpPost("EmailExists")]
    public async Task<bool> EmailExists([FromBody] EmailExistsRequest request)
    {
        return await authenticationService.EmailExistsAsync(request);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
    {
        var result = await authenticationService.LoginUserAsync(request);

        return result;
    }

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public async Task<ForgotPasswordResponse> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        return await authenticationService.ForgotPasswordAsync(request);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<UserRegisterResponse> Register([FromBody] UserRegisterRequest user)
    {
        return await authenticationService.RegisterUserAsync(user);
    }

    [AllowAnonymous]
    [HttpPost("SetPassword")]
    public async Task SetPassword([FromBody] SetPasswordRequest request)
    {
        await authenticationService.SetPasswordAsync(request);
    }
}

