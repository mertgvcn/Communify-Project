using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.NonPersistentModels.ViewModels;

namespace LethalCompany_Backend.Services.Interfaces;
public interface IProfilePageService
{
    Task<UserInformationSummaryViewModel> GetUserInformationSummaryAsync(string username);
    Task<ProfileStatsViewModel> GetProfileStatsAsync(string username);
    Task<ProfileStatusViewModel> GetProfileStatusAsync(string username);
    Task<UserInformationViewModel> GetUserInformationAsync();
    Task<bool> ToggleFollowUserAsync(FollowUserRequest request);
    Task<bool> IsProfileOwnerAsync(string username);
    Task<bool> IsFollowerAsync(string username);
}