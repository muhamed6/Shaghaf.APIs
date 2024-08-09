using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
 
    public class PhotoSessionsController : BaseApiController
    {
        private readonly IPhotoSessionService _photoSessionService;
        private readonly IMapper _mapper;

        public PhotoSessionsController(IMapper mapper,
            IPhotoSessionService photoSessionService )
        {
            _mapper = mapper;
            _photoSessionService = photoSessionService;
           
        }

        [HttpGet("{photoSessionId}")]
        [ProducesResponseType(typeof(PhotoSessionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoSessionDto?>> GetPhotoSessionById(int photoSessionId)
        {
            var photoSession = await _photoSessionService.GetPhotoSessionDetailsAsync(photoSessionId);

            if (photoSession is null)
                return NotFound("This PhotoSession Does not exist!!");

            return Ok(photoSession);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PhotoSessionDto>>> GetAllPhotoSessions()
        {
            var photoSessions = await _photoSessionService.GetAllPhotoSessionsAsync();
            if (photoSessions.Count < 1)
            {
                return NotFound("There is no photoSession!!");
            }
            return Ok(_mapper.Map<IReadOnlyList<PhotoSessionDto>>(photoSessions));
        }


        [HttpPost]
        public async Task<ActionResult<PhotoSessionDto?>> CreatePhotoSession([FromBody] PhotoSessionDto photoSessionDto)
        {

            var result = await _photoSessionService.CreatePhotoSessionAsync (photoSessionDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }

        [HttpPost("photoSessionId")]
        public async Task<ActionResult<PhotoSessionDto?>> UpdatePhotoSession(int photoSessionId, [FromBody] PhotoSessionDto photoSessionDto)
        {

            var result = await _photoSessionService.UpdatePhotoSessionAsync(photoSessionId, photoSessionDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePhotoSession(int photoSessionId)
        {
            var isDeleted = await _photoSessionService.Delete(photoSessionId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
