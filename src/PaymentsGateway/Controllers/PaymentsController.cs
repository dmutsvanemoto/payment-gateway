using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentsGateway.Models;
using PaymentsGateway.Services;

namespace PaymentsGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentsService _paymentsService;
        private readonly IBankService _bankService;

        public PaymentsController(ILogger<PaymentsController> logger, IPaymentsService paymentsService, IBankService bankService)
        {
            _logger = logger;
            _paymentsService = paymentsService;
            _bankService = bankService;
        }

        [HttpPut]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _bankService.ProcessPayment(payload);

                var paymentCreated = await _paymentsService.ProcessPayment(payload);

                return paymentCreated ? NoContent() : BadRequest();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to process payment!");
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("")]
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