using Microsoft.AspNetCore.Mvc;
using Mudemy.Core.DTOs;
using Mudemy.Core.Services;
using SharedLibrary.Dtos;
using System.Threading.Tasks;

namespace Mudemy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentDto paymentDto, int orderId)
        {
            if (paymentDto == null || orderId <= 0)
            {
                return BadRequest(Response<string>.Fail("Invalid request data", 400, true));
            }

            var result = await _paymentService.ProcessPaymentAsync(paymentDto, orderId);

            if (!result.IsSuccessful)
            {
                return StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }
    }
}
