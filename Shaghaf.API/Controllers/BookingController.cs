using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Services.Contract;
using Stripe;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IPaymentService _paymentService;
    // private const string WebhookSecret = "whsec_8wX0XPHOBXyuPGs7E0BqDPpgc7lcheFs";

    // Constructor to initialize the booking and payment services
    public BookingController(IBookingService bookingService, IPaymentService paymentService)
    {
        _bookingService = bookingService;
        _paymentService = paymentService;
    }

    // Endpoint to create a new booking
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

    // Endpoint to get the details of a specific booking by ID
    [HttpGet("{bookingId}")]
    public async Task<ActionResult<BookingDto>> GetBookingDetails(int bookingId)
    {
        // Retrieve the booking details asynchronously using the booking service
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

    // Endpoint to get the details of all bookings
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAllBookingDetails()
    {
        // Retrieve all booking details asynchronously using the booking service
        var result = await _bookingService.GetAllBookingDetailsAsync();

        if (result is null)
            return NotFound("Booking Not Found !!");

   
        return Ok(result);
    }

    // Endpoint to create a payment session for a booking
    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
    {
        try
        {
            // Create a checkout session asynchronously using the payment service
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
