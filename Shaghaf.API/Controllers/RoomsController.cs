using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shaghaf.API.Controllers
{
   
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
        public async Task<ActionResult<RoomDto?>> CreateRoom(RoomToCreateDto model)
        {
            var room = await _roomService.CreateRoomAsync(model);

            if (room is null)
                return BadRequest("Invalid Create!!");

            return Ok(_mapper.Map<Room, RoomDto>(room));
        }
    }
}
