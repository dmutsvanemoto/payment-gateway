using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using PaymentsAPI.Data.Models.Requests;

namespace PaymentsAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class WriteController : ControllerBase
    {
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
