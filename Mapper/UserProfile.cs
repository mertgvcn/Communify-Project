using AutoMapper;
using CommunifyLibrary.Models;
using CommunifyLibrary.NonPersistentModels.ViewModels;

namespace LethalCompany_Backend.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, SearchedUserViewModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

        CreateMap<User, UserInformationViewModel>();

        CreateMap<User, UserInformationSummaryViewModel>();
    }
}
