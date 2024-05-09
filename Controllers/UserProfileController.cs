using CommunifyLibrary.NonPersistentModels.ViewModels;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : Controller
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet("GetUserInformation")]
    public async Task<UserInformationViewModel> GetUserInformation()
    => await _userProfileService.GetUserInformationAsync();
}
