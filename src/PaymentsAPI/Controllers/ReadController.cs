using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentsAPI.Business.Services;

namespace PaymentsAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        private readonly ILogger<ReadController> _logger;
        private readonly IReadService _readService;

        public ReadController(ILogger<ReadController> logger, IReadService readService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _readService = readService ?? throw new ArgumentNullException(nameof(readService));
        }

        [HttpGet]
        [Route("")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPaymentsByMerchantId([FromQuery] int? merchantId)
        {
            try
            {
                if (!merchantId.HasValue)
                {
                    return BadRequest($"{nameof(merchantId)} is required");
                }

                var payments = await _readService.GetByMerchantId(merchantId.Value);

                return Ok(payments);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to get payments using merchantId = {merchantId}");
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
