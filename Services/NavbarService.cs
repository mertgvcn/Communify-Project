using CommunifyLibrary.NonPersistentModels.ParameterModels;
using CommunifyLibrary.Repository;

namespace LethalCompany_Backend.Services;

public class NavbarService
{
    private readonly IUserRepository _userRepository;

    public NavbarService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<object>> Search(SearchRequest request)
    {

        return null;
    }
}
