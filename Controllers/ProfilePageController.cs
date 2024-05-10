using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.NonPersistentModels.ViewModels;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProfilePageController : Controller
{
    private readonly IProfilePageService _userProfileService;

    public ProfilePageController(IProfilePageService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<JsonResult> GetProfilePageData(string username)
    {
        var userInformationSummary = await _userProfileService.GetUserInformationSummaryAsync(username);
        var isOwner = await _userProfileService.IsProfileOwnerAsync(username);
        var isFollower = isOwner ? false : await _userProfileService.IsFollowerAsync(username);

        var response = new
        {
            userInformationSummary = userInformationSummary,
            profileStatus = new
            {
                isOwner = isOwner,
                isFollower = isFollower,
            }
        };

        return new JsonResult(response);
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<UserInformationViewModel> GetUserInformation()
        => await _userProfileService.GetUserInformationAsync();

    [HttpPost]
    [AllowAnonymous]
    public async Task<bool> ToggleFollowUser(FollowUserRequest request)
        => await _userProfileService.ToggleFollowUserAsync(request);
}
