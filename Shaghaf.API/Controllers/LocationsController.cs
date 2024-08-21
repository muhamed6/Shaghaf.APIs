using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
    [Authorize]
    public class LocationsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;

        public LocationsController(IMapper mapper,
            ILocationService locationService)
        {
            _mapper = mapper;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Location>>> GetAllLocations()
        {

            var result = await _locationService.GetAllLocationsAsync();

            if (result.Count == 0)
                return NotFound("There is no Location!!");


            return Ok(result);
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<Location?>> GetLocationDetails(int locationId)
        {

            var result = await _locationService.GetLocationDetailsAsync(locationId);

            if (result is null)
                return NotFound("Location Not Found!!");

            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Location?>> CreateLocation([FromBody] LocationDto locationDto)
        {

            var result = await _locationService.CreateLocationAsync(locationDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("locationId")]
        public async Task<ActionResult<Location?>> UpdateLocation(int locationId, [FromBody] LocationDto locationDto)
        {



            var result = await _locationService.UpdateLocationAsync(locationId, locationDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            var isDeleted = await _locationService.Delete(locationId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }


    }
}
