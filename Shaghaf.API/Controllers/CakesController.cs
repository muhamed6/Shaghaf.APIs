using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{

    public class CakesController : BaseApiController
    {
        private readonly ICakeService _cakeService;
        private readonly IMapper _mapper;

        public CakesController(ICakeService cakeService, IMapper mapper)
        {
            _cakeService = cakeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CakeDto?>> CreateCake([FromBody] CakeDto cakeDto)
        {

            var result = await _cakeService.CreateCakeAsync(cakeDto);

            if (result is null)
            {
                return BadRequest("Invalid Data !!");

            }
            return Ok(result);
        }

        [HttpPost("cakeId")]
        public async Task<ActionResult<CakeDto?>> UpdateCake(int cakeId, [FromBody] CakeDto cakeDto)
        {

            var result = await _cakeService.UpdateCakeAsync(cakeId, cakeDto);

            if (result is null)
            {
                return BadRequest("Invalid Data !!");

            }
            return Ok(result);
        }

        [HttpGet("{cakeId}")]
        public async Task<ActionResult<CakeDto?>> GetCakeDetails(int cakeId)
        {

            var result = await _cakeService.GetCakeDetailsAsync(cakeId);

            if (result is null)
                return NotFound("Cake Not Found !!");

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CakeDto>>> GetAllCakes()
        {

            var result = await _cakeService.GetAllCakesAsync();

            if (result.Count == 0)
                return NotFound("There is no Cake !!");


            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCake(int cakeId)
        {
            var isDeleted = await _cakeService.Delete(cakeId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
