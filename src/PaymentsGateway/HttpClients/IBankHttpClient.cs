using System.Net.Http;
using System.Threading.Tasks;
using PaymentsGateway.Models.BankAPI;

namespace PaymentsGateway.HttpClients
{
    public interface IBankHttpClient
    {
        /// <summary>
        /// Authenticate with Bank Auth API to permit Gateway to perform
        /// transactions on behalf of a Merchant
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> Authenticate(string clientId, string clientSecret);

        /// <summary>
        /// Verify Card Details and Process payment between Shopper and Merchant
        /// </summary>
        /// <param name="processPaymentRequest"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> ProcessPayment(ProcessPaymentRequest processPaymentRequest);
    }
}
