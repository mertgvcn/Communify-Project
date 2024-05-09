using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommunifyLibrary.NonPersistentModels.ViewModels;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LethalCompany_Backend.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextService _httpContextService;
    private readonly IMapper _mapper;

    public UserProfileService(IUserRepository userRepository, IHttpContextService httpContextService, IMapper mapper)
    {
        _userRepository = userRepository;
        _httpContextService = httpContextService;
        _mapper = mapper;
    }

    public async Task<bool> IsProfileOwnerAsync(string username)
    {
        var userId = _httpContextService.GetCurrentUserID();
        if (userId == -1) return false;

        return (await _userRepository.GetByIdAsync(userId)).Username == username;
    }

    public async Task<UserInformationViewModel> GetUserInformationAsync()
    {
        var userId = _httpContextService.GetCurrentUserID();
        var userInformation = await _userRepository.GetAll().Where(u => u.Id == userId)
                    .ProjectTo<UserInformationViewModel>(_mapper.ConfigurationProvider)
                    .SingleAsync();

        return userInformation;
    }
}
