using AutoMapper;
using CommunifyLibrary.Models;
using LethalCompany_Backend.Models.InterestModels;

namespace LethalCompany_Backend.Mapper;

public class InterestProfile : Profile
{
    public InterestProfile()
    {
        CreateMap<Interest, InterestViewModel>();
    }
}
