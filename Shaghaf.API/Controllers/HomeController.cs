using Microsoft.AspNetCore.Mvc;
using Shaghaf.Application.Services;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
   
    public class HomeController : BaseApiController
    {
        private readonly IHomeService _homeService;
        private readonly IGenericRepository<Home> _homeRepo;

       
        public HomeController(IHomeService homeService, IGenericRepository<Home> homeRepo)
        {
            _homeService = homeService;
            _homeRepo = homeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomeData()
        {
           
            var homeData = await _homeService.GetHomeDataAsync();
            return Ok(homeData);
        }

        
        [HttpGet("memberships")]
        public async Task<ActionResult<List<MembershipDto>>> GetMemberships()
        {
          
            var memberships = await _homeService.GetMembershipsAsync();
            return Ok(memberships);
        }

      
        [HttpGet("birthdays")]
        public async Task<ActionResult<List<BirthdayDto>>> GetBirthdays()
        {
            
            var birthdays = await _homeService.GetBirthdaysAsync();
           
            return Ok(birthdays);
        }

       
        [HttpGet("photosessions")]
        public async Task<ActionResult<List<PhotoSessionDto>>> GetPhotoSessions()
        {
           
            var photoSessions = await _homeService.GetPhotoSessionsAsync();
            return Ok(photoSessions);
        }

        
        [HttpGet("advertisements")]
        public async Task<ActionResult<List<AdvertisementDto>>> GetAdvertisements()
        {
            // Retrieve advertisements asynchronously using the home service
            var advertisements = await _homeService.GetAdvertisementsAsync();
            return Ok(advertisements);
        }

      
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories() 
        {
        
            var categories = await _homeService.GetCategoriesAsync();
           
            return Ok(categories);
        }
    }
}
