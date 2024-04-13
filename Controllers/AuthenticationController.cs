﻿using Communify_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Communify_Backend.Models.AuthenticationModels;

namespace Communify_Backend.Controllers
{
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
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody] UserLoginRequest request)
        {
            var result = await authenticationService.LoginUserAsync(request);

            return result;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<UserRegisterResponse> Register([FromBody] UserRegisterRequest user)
        {
            return await authenticationService.RegisterUserAsync(user);
        }

        [AllowAnonymous]
        [HttpPost("isEmailAvailable")]
        public async Task<bool> isEmailAvailable([FromBody] isEmailAvailableRequest request)
        {
            return await authenticationService.isEmailAvailable(request);
        }

        [Authorize(Roles = "unAuthorizedUser")]
        [HttpPost("SetPassword")]
        public async Task SetPassword([FromBody] SetPasswordRequest request)
        {
            await authenticationService.SetPassword(request);
        }
    }
}
