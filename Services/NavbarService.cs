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
    private readonly IMapper _mapper;

    public NavbarService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<SearchedUserViewModel>> SearchAsync(SearchRequest request)
    {
        if (request.Input.Trim() == "") return [];

        //To Do : Daha sonra postlara göre, communitylere göre arama seçenekleri eklenecek
        var searchResult = await _userRepository.SearchUsers(request.Input)
                                    .ProjectTo<SearchedUserViewModel>(_mapper.ConfigurationProvider).Take(5).ToListAsync();
        return searchResult;
    }
}
