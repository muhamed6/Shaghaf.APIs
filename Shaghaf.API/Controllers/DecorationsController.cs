using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Application.Services;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
    [Authorize]
    public class DecorationsController : BaseApiController
    {
        private readonly IDecorationService _decorationService;
        private readonly IMapper _mapper;

        public DecorationsController(IDecorationService decorationService, IMapper mapper)
        {
            _decorationService = decorationService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DecorationDto?>> CreateDecoration([FromBody] DecorationDto decorationDto)
        {

            var result = await _decorationService.CreateDecorationAsync(decorationDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("decorationId")]
        public async Task<ActionResult<DecorationDto?>> UpdateDecoration(int decorationId, [FromBody] DecorationDto decorationDto)
        {

            var result = await _decorationService.UpdateDecorationAsync(decorationId, decorationDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }


        [HttpGet("{decorationId}")]
        public async Task<ActionResult<DecorationDto?>> GetDecorationDetails(int decorationId)
        {

            var result = await _decorationService.GetDecorationDetailsAsync(decorationId);

            if (result is null)
                return NotFound("Decoration Not Found!!");

            return Ok(_mapper.Map<DecorationDto>(result));
        }



        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DecorationDto>>> GetAllDecorations()
        {
            var result = await _decorationService.GetAllDecorationsAsync();

            if (result.Count == 0)
                return NotFound("There is no  Decoration!!");


            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDecoration(int decorationId)
        {
            var isDeleted = await _decorationService.Delete(decorationId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
