using CommunifyLibrary.NonPersistentModels.ViewModels;

namespace LethalCompany_Backend.Services.Interfaces;
public interface IUserProfileService
{
    Task<bool> IsProfileOwnerAsync(string username);
    Task<UserInformationViewModel> GetUserInformationAsync();
}