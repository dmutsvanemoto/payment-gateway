using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PaymentsGateway.Models;

namespace PaymentsGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        [Route("payments")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest();
        }
    }
}