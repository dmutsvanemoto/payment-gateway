using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentsAPI.Business.Services;
using PaymentsAPI.Data.Models.Requests;

namespace PaymentsAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        private readonly ILogger<WriteController> _logger;
        private readonly IWriteService _writeService;

        public WriteController(ILogger<WriteController> logger, IWriteService writeService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _writeService = writeService ?? throw new ArgumentNullException(nameof(writeService));
        }

        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Failed to create payment");
                }

                await _writeService.CreatePayment(request);

                return NoContent();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to create payment!");
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
