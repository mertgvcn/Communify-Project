using CommunifyLibrary.NonPersistentModels.ViewModels;

namespace LethalCompany_Backend.Services.Interfaces;
public interface IUserProfileService
{
    Task<UserInformationViewModel> GetUserInformationAsync();
}