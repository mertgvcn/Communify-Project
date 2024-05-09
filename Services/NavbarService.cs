using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.NonPersistentModels.ViewModels;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LethalCompany_Backend.Services;

public class NavbarService : INavbarService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextService _httpContextService;
    private readonly IMapper _mapper;

    public NavbarService(IUserRepository userRepository, IHttpContextService httpContextService, IMapper mapper)
    {
        _userRepository = userRepository;
        _httpContextService = httpContextService;
        _mapper = mapper;
    }

    public async Task<List<SearchedUserViewModel>> SearchAsync(SearchRequest request)
    {
        if (request.Input.Trim() == "") return [];

        //To Do : Daha sonra postlara göre, communitylere göre arama seçenekleri eklenecek
        var searchResult = await _userRepository.SearchUsers(request.Input)
                                    .ProjectTo<SearchedUserViewModel>(_mapper.ConfigurationProvider)
                                    .Take(5).ToListAsync();
        return searchResult;
    }

    public async Task<string> GetUsernameAsync()
    {
        var userId = _httpContextService.GetCurrentUserID();

        return (await _userRepository.GetByIdAsync(userId)).Username;
    }

    public async Task<UserInformationSummaryViewModel> GetUserInformationSummaryAsync(string username)
    {
        var userInformationSummary = await _userRepository.GetAll().Where(u => u.Username == username)
                                                .ProjectTo<UserInformationSummaryViewModel>(_mapper.ConfigurationProvider)
                                                .SingleAsync();

        return userInformationSummary;
    }
}
