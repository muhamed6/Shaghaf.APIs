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
        
        public BirthDaysController(IBirthDayService birthDayService
         )
        {
            _birthDayService = birthDayService;
        
        }


        [HttpPost]
        public async Task<ActionResult<BirthDayToCreateDto?>> CreateBirthday([FromBody] BirthDayToCreateDto birthdayDto)
        {

            var result = await _birthDayService.CreateBirthDayAsync(birthdayDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }

         [HttpPost("birthdayId")]
        public async Task<ActionResult<BirthDayToCreateDto?>> UpdateBirthday(int birthdayId, [FromBody] BirthDayToCreateDto birthdayDto)
        {
  
          

            var result = await _birthDayService.UpdateBirthDayAsync(birthdayId, birthdayDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }



        [HttpGet("{birthdayId}")]
        public async Task<ActionResult<Birthday?>> GetBirthDayDetails(int birthdayId)
        {
      
            var result = await _birthDayService.GetBirthDayDetailsAsync(birthdayId);

            if (result is null)
                return NotFound("Birthday Not Found!!");

            return Ok(result);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Birthday>>> GetAllBirthDays()
        {
            var result = await _birthDayService.GetAllBirthDaysAsync();

            if (result.Count == 0)
                return NotFound("There is no any BirthDay!!");


            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteBirthDay(int birthdayId)
        {
            var isDeleted = await _birthDayService.Delete(birthdayId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
