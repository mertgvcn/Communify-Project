using Communify_Backend.Services.Interfaces;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PasswordTokenController : Controller
{
    private readonly ITokenService _tokenService;

    public PasswordTokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }


    [HttpPost("PasswordTokenExists")]
    public async Task<bool> PasswordTokenExists(PasswordTokenExistsRequest request)
        => await _tokenService.PasswordTokenExistsAsync(request);
}
