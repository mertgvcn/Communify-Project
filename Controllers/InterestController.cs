using AutoMapper;
using AutoMapper.QueryableExtensions;
using CommunifyLibrary.Repository;
using LethalCompany_Backend.Models.InterestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LethalCompany_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class InterestController : Controller
    {
        private readonly IInterestRepository _interestRepository;
        private readonly IMapper _mapper;

        public InterestController(IInterestRepository interestRepository, IMapper mapper)
        {
            _interestRepository = interestRepository;
            _mapper = mapper;
        }

        [HttpGet("GetInterests")]
        public async Task<List<InterestViewModel>> GetInterests()
        {
            var interests = await _interestRepository.GetAll()
                .ProjectTo<InterestViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return interests;
        }
    }
}
