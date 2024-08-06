using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
    
    public class BirthDaysController : BaseApiController
    {
        private readonly IBirthDayService _birthDayService;
        private readonly IHomeService _homeService;
        public BirthDaysController(IBirthDayService birthDayService,
            IHomeService homeService)
        {
            _birthDayService = birthDayService;
            _homeService= homeService;
        }


        [HttpPost]
        public async Task<ActionResult<BirthDayToCreateDto?>> CreateBirthday([FromBody] BirthDayToCreateDto birthdayDto)
        {

            var result = await _birthDayService.CreateBirthDayAsync(birthdayDto);

            if (result is null)
            {
                return BadRequest("Invalid Data !!");

            }
            return Ok(result);
        }

         [HttpPost("birthdayId")]
        public async Task<ActionResult<BirthDayToCreateDto?>> UpdateBirthday(int id, [FromBody] BirthDayToCreateDto birthdayDto)
        {
  
          

            var result = await _birthDayService.UpdateBirthDayAsync(id, birthdayDto);

            if (result is null)
            {
                return BadRequest("Invalid Data !!");

            }
            return Ok(result);
        }



        [HttpGet("{birthdayId}")]
        public async Task<ActionResult<BirthdayDto>> GetBirthDayDetails(int birthdayId)
        {
            // Retrieve the booking details asynchronously using the booking service
            var result = await _birthDayService.GetBirthDayDetailsAsync(birthdayId);

            if (result is null)
                return NotFound("Birthday Not Found !!");

            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BirthdayDto>>> GetAllBirthDays()
        {
            // Retrieve all booking details asynchronously using the booking service
            var result = await _homeService.GetBirthdaysAsync();

            if (result.Count==0)
                return NotFound("There is no any BirthDay!!");


            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteBirthDay(int id)
        {
            var isDeleted = await _birthDayService.Delete(id);
            return isDeleted ? Ok("Delted") : BadRequest("Invalid Operation");
        }
    }
}
