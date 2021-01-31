using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace PaymentsAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPaymentsByMerchantId([FromRoute] int? merchantId)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
