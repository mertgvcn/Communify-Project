using CommunifyLibrary.NonPersistentModels.ViewModels;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserProfileController : Controller
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<UserInformationViewModel> GetUserInformation()
        => await _userProfileService.GetUserInformationAsync();

    [HttpGet]
    [AllowAnonymous]
    public async Task<bool> IsProfileOwner(string username)
        => await _userProfileService.IsProfileOwnerAsync(username);
}
