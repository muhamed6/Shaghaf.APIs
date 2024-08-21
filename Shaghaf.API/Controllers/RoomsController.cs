using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Stripe.Climate;

namespace Shaghaf.API.Controllers
{
    [Authorize]
    public class RoomsController : BaseApiController
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        
        public RoomsController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            if(rooms.Count <1) 
            {
               return NotFound("There is no room!!");
            }
            return Ok(_mapper.Map<IReadOnlyList<Room>, IReadOnlyList<RoomDto>>(rooms));
        }

        [HttpGet("AllRoomsForUser")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAllRoomsForUser(string phoneNumber)
        {
            var rooms = await _roomService.GetRoomsForUserAsync(phoneNumber);
            return Ok(_mapper.Map<IReadOnlyList<Room>, IReadOnlyList<RoomDto>>(rooms));

        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomDto>> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomById(id);

            if (room is null)
                return NotFound("This Room Does not exist!!");

            return Ok(_mapper.Map<Room, RoomDto>(room));
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto?>> CreateRoom(RoomToCreateOrUpdateDto roomDto)
        {
            var phoneNumber = User.FindFirstValue(ClaimTypes.MobilePhone);
            var room = await _roomService.CreateRoomAsync(phoneNumber, roomDto);

            if (room is null)
                return BadRequest("Invalid Create!!");

            return Ok(room);
        }
       
        [HttpPost("roomId")]
        public async Task<ActionResult<RoomDto?>> UpdateRoom(int roomId, [FromBody] RoomToCreateOrUpdateDto roomDto)
        {


            var phoneNumber = User.FindFirstValue(ClaimTypes.MobilePhone);
            var result = await _roomService.UpdateRoomAsync(roomId,phoneNumber, roomDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(_mapper.Map<RoomDto>(result));
        }
       
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var isDeleted = await _roomService.Delete(roomId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
