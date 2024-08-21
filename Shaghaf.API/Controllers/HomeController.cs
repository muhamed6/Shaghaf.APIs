using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Application.Services;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{

    public class HomeController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IDecorationService _decorationService;
        private readonly ICakeService _cakeService;
        private readonly IBookingService _bookingService;
        private readonly IBirthDayService _birthDayService;
        private readonly ILocationService _locationService;
        private readonly IRoomService _roomService;

        public HomeController(IMapper mapper,
            IDecorationService decorationService,
            ICakeService cakeService,
            IBookingService bookingService,
            IBirthDayService birthDayService,
            ILocationService locationService,
            IRoomService roomService



            )
        {
            _mapper = mapper;
            _decorationService = decorationService;
            _cakeService = cakeService;
            _bookingService = bookingService;
            _birthDayService = birthDayService;
            _locationService = locationService;
            _roomService = roomService;
        }

        [HttpGet("BirthDays")]
        public async Task<ActionResult<IReadOnlyList<BirthdayDto>>> GetAllBirthDays()
        {
            var result = await _birthDayService.GetAllBirthDaysAsync();

            if (result.Count == 0)
                return NotFound("There is no any BirthDay!!");


            return Ok(_mapper.Map<IReadOnlyList<Birthday>, IReadOnlyList<BirthdayDto>>(result));
        }


        [HttpGet("Rooms")]
        public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            if (rooms.Count < 1)
            {
                return NotFound("There is no room!!");
            }
            return Ok(_mapper.Map<IReadOnlyList<RoomDto>>(rooms));
        }

        [HttpGet("Locations")]
        public async Task<ActionResult<IReadOnlyList<Location>>> GetAllLocations()
        {

            var result = await _locationService.GetAllLocationsAsync();

            if (result.Count == 0)
                return NotFound("There is no Location!!");


            return Ok(result);
        }

        [HttpGet("Decorations")]
        public async Task<ActionResult<IReadOnlyList<DecorationDto>>> GetAllDecorations()
        {
            var result = await _decorationService.GetAllDecorationsAsync();

            if (result.Count == 0)
                return NotFound("There is no  Decoration!!");


            return Ok(result);
        }

        [HttpGet("Cakes")]
        public async Task<ActionResult<IReadOnlyList<CakeDto>>> GetAllCakes()
        {

            var result = await _cakeService.GetAllCakesAsync();

            if (result.Count == 0)
                return NotFound("There is no Cake !!");


            return Ok(result);
        }

        [HttpGet("Bookings")]
        public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAllBookingDetails()
        {

            var result = await _bookingService.GetAllBookingDetailsAsync();

            if (result.Count == 0)
                return NotFound("There is no Booking !!");


            return Ok(result);
        }



    }
}
