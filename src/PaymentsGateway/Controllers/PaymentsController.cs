using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PaymentsGateway.Models;
using PaymentsGateway.Services;

namespace PaymentsGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        [HttpPost]
        [Route("payments")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var paymentCreated = await _paymentsService.ProcessPayment(payload);

            return paymentCreated ? NoContent() : BadRequest();
        }

        [HttpGet]
        [Route("payments")]
        public async Task<IActionResult> RetrievePayments([FromQuery] int merchantId)
        {
            if (merchantId == 0)
            {
                return BadRequest();
            }

            var payments = await _paymentsService.RetrievePayments(merchantId);
            
            return Ok(payments);
        }
    }
}