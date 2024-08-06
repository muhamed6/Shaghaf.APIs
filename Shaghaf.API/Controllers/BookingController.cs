using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Stripe;
using System;
using System.Threading.Tasks;
using Talabat.APIs.Controllers;


public class BookingController : BaseApiController
{
    private readonly IBookingService _bookingService;
    private readonly IPaymentService _paymentService;

    public BookingController(IBookingService bookingService, IPaymentService paymentService)
    {
        _bookingService = bookingService;
        _paymentService = paymentService;
    }

  
    [HttpPost]
    public async Task<ActionResult<BookingDto?>> CreateBooking([FromBody] BookingDto bookingDto)
    {
        var result = await _bookingService.CreateBookingAsync(bookingDto);

        if (result is null)
        {
            return BadRequest("Invalid Data !!");

        }
        return Ok(result);
    }

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

    [HttpDelete]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var isDeleted = await _bookingService.Delete(id);
        return isDeleted ? Ok("Delted") : BadRequest("Invalid Operation");
    }

   
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAllBookingDetails()
    {
       
        var result = await _bookingService.GetAllBookingDetailsAsync();

        if (result.Count == 0)
            return NotFound("There is no Booking !!");

   
        return Ok(result);
    }

   
    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
    {
        try
        {
            
            var session = await _paymentService.CreateCheckoutSession(paymentDto);
            // Return the URL of the created payment session
            return Ok(new { url = session.Url });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Endpoint to check the payment status of a specific booking by ID
    [HttpGet("payment/check-status/{bookingId}")]
    public async Task<IActionResult> GetPaymentStatus(int bookingId)
    {
        try
        {
            // Check the payment status asynchronously using the payment service
            var status = await _paymentService.CheckPaymentStatusAsync(bookingId);
            // Return the payment status of the booking
            return Ok(new { status });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
  


}
