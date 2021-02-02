using System.Threading.Tasks;
using PaymentsGateway.Models;

namespace PaymentsGateway.Services
{
    public interface IBankService
    {
        /// <summary>
        /// Authenticate Gateway with Bank and process payment on behalf of Merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task ProcessPayment(PaymentRequest request);
    }
}
