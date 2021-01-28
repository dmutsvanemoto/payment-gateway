using System.Net.Http;
using System.Threading.Tasks;
using PaymentsGateway.Models;

namespace PaymentsGateway.HttpClients
{
    public interface IPaymentsApiClient
    {
        Task<HttpResponseMessage> CreatePayment(PaymentRequest payload);
    }
}
