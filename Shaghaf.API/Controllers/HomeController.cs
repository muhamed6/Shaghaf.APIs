//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Shaghaf.Application.Services;
//using Shaghaf.Core.Dtos;
//using Shaghaf.Core.Entities.HomeEntities;
//using Shaghaf.Core.Repositories.Contract;
//using Shaghaf.Core.Services.Contract;
//using Shaghaf.Service;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Talabat.APIs.Controllers;

//namespace Shaghaf.API.Controllers
//{

//    public class HomeController : BaseApiController
//    {
//        private readonly IMapper _mapper;
//        private readonly IDecorationService _decorationService;
//        private readonly ICakeService _cakeService;
//        private readonly IBookingService _bookingService;
//        private readonly IBirthDayService _birthDayService;
//        private readonly ILocationService _locationService;
//        private readonly IRoomService _roomService;

//        public HomeController(IMapper mapper,
//            IDecorationService decorationService,
//            ICakeService cakeService,
//            IBookingService bookingService,
//            IBirthDayService birthDayService,
//            ILocationService locationService,
//            IRoomService roomService



//            )
//        {
//            this.mapper = mapper;
//            this.decorationService = decorationService;
//            this.cakeService = cakeService;
//            this.bookingService = bookingService;
//            this.birthDayService = birthDayService;
//            this.locationService = locationService;
//            this.roomService = roomService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IReadOnlyList<Birthday>>> GetAllBirthDays()
//        {
//            var result = await _birthDayService.GetAllBirthDaysAsync();

//            if (result.Count == 0)
//                return NotFound("There is no any BirthDay!!");


//            return Ok(result);
//        }

//    }
//}
