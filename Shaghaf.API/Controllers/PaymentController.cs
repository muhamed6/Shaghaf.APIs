using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Services.Contract;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
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
        [HttpGet]
        public async Task<IActionResult> GetPaymentStatus(int bookingId)
        {
            try
            {
                // Check the payment status asynchronously using the payment service
                var status = await _paymentService.CheckPaymentStatusAsync(bookingId);

                return Ok(new { status });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
