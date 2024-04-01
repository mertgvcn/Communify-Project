using CommunifyLibrary;
using LethalCompany_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LethalCompany_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class InterestController : Controller
    {
        private readonly CommunifyContext context;

        public InterestController(CommunifyContext context)
        {
            this.context = context;
        }

        [HttpGet("GetInterests")]
        public async Task<JsonResult> GetInterests()
        {
            var interests = context.Interests.Select(a => new InterestViewModel
            {
                Id = a.Id,
                Name = a.Name,
                IsChecked = false
            }).ToList();

            return new JsonResult(interests);
        }
    }
}
