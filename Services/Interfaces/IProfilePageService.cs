using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.NonPersistentModels.ViewModels;

namespace LethalCompany_Backend.Services.Interfaces;
public interface IProfilePageService
{
    Task<bool> IsProfileOwnerAsync(string username);
    Task<bool> IsFollowerAsync(string username);
    Task<UserInformationSummaryViewModel> GetUserInformationSummaryAsync(string username);
    Task<UserInformationViewModel> GetUserInformationAsync();
    Task<bool> ToggleFollowUserAsync(FollowUserRequest request);
}