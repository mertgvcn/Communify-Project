using CommunifyLibrary.Repository.Interfaces;
using LethalCompany_Backend.NonPersistentModels.TokenModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PasswordTokenController : Controller
{
    private readonly IPasswordTokenRepository _passwordTokenRepository;

    public PasswordTokenController(IPasswordTokenRepository passwordTokenRepository)
    {
        _passwordTokenRepository = passwordTokenRepository;
    }


    [HttpPost("GetPasswordTokenByToken")]
    public async Task<JsonResult> GetPasswordTokenByToken(GetPasswordTokenByTokenRequest request)
    {
        var passwordToken = await _passwordTokenRepository.GetByTokenAsync(request.Token);

        return new JsonResult(passwordToken);
    }
}
