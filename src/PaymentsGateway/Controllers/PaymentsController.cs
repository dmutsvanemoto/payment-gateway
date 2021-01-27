using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PaymentsGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        [Route("payments")]
        public async Task<IActionResult> ProcessPayment()
        {   
            return BadRequest();
        }
    }
}