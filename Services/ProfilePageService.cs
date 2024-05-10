using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.NonPersistentModels.ViewModels;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LethalCompany_Backend.Services;

public class ProfilePageService : IProfilePageService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextService _httpContextService;
    private readonly IMapper _mapper;

    public ProfilePageService(IUserRepository userRepository, IHttpContextService httpContextService, IMapper mapper)
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

    public async Task<bool> IsFollowerAsync(string username)
    {
        var userId = _httpContextService.GetCurrentUserID();
        if (userId == -1) return false;

        var user = await _userRepository.GetAll().Where(u => u.Id == userId).Include(u => u.Followings).SingleAsync();
        var following = await _userRepository.GetByUsername(username).SingleAsync();

        var isFollower = user.Followings.Contains(following);

        return isFollower;
    }
    public async Task<UserInformationSummaryViewModel> GetUserInformationSummaryAsync(string username)
    {
        var userInformationSummary = await _userRepository.GetAll().Where(u => u.Username == username)
                                                .ProjectTo<UserInformationSummaryViewModel>(_mapper.ConfigurationProvider)
                                                .SingleAsync();

        return userInformationSummary;
    }

    public async Task<UserInformationViewModel> GetUserInformationAsync()
    {
        var userId = _httpContextService.GetCurrentUserID();
        var userInformation = await _userRepository.GetAll().Where(u => u.Id == userId)
                    .ProjectTo<UserInformationViewModel>(_mapper.ConfigurationProvider)
                    .SingleAsync();

        return userInformation;
    }

    public async Task<bool> ToggleFollowUserAsync(FollowUserRequest request)
    {
        var userId = _httpContextService.GetCurrentUserID();
        if (userId == -1) return false;

        var user = await _userRepository.GetAll().Where(u => u.Id == userId).Include(u => u.Followings).SingleAsync();
        var following = await _userRepository.GetByUsername(request.Username).Include(f => f.Followers).SingleAsync();

        if (request.IsFollower)
        {
            user.Followings.Remove(following);
            following.Followers.Remove(user);
        }
        else
        {
            user.Followings.Add(following);
            following.Followers.Add(user);
        }

        await _userRepository.UpdateAsync(user);
        await _userRepository.UpdateAsync(following);

        //TODO: Notification gönder

        return true;
    }
}
