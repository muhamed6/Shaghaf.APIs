using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Stripe;
using System;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;

[Authorize]
public class BookingController : BaseApiController
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BookingDto?>> CreateBooking([FromBody] BookingDto bookingDto)
    {
        var result = await _bookingService.CreateBookingAsync(bookingDto);

        if (result is null)
        {
            return BadRequest("Invalid Data!!");

        }
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("bookingId")]
    public async Task<ActionResult<BookingDto?>> UpdateBooking(int bookingId, [FromBody] BookingDto bookingDto)
    {



        var result = await _bookingService.UpdateBookingAsync(bookingId, bookingDto);

        if (result is null)
        {
            return BadRequest("Invalid Data !!");

        }
        return Ok(result);
    }



    [HttpGet("{bookingId}")]
    public async Task<ActionResult<BookingDto>> GetBookingDetails(int bookingId)
    {
  
        var result = await _bookingService.GetBookingDetailsAsync(bookingId);

        if (result is null)
            return NotFound("Booking Not Found !!");

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var isDeleted = await _bookingService.Delete(id);
        return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation");
    }

   
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAllBookingDetails()
    {
       
        var result = await _bookingService.GetAllBookingDetailsAsync();

        if (result.Count == 0)
            return NotFound("There is no Booking !!");

   
        return Ok(result);
    }

   
   
  


}
