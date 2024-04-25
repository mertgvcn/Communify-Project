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

        public InterestController(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        [HttpGet("GetInterests")]
        public async Task<JsonResult> GetInterests()
        {
            var interests = await _interestRepository.GetAll().Select(a => new InterestViewModel
            {
                Id = a.Id,
                Name = a.Name,
                IsChecked = false
            }).ToListAsync();

            return new JsonResult(interests);
        }
    }
}
