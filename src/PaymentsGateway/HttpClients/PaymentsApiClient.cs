using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PaymentsGateway.Models;

namespace PaymentsGateway.HttpClients
{
    public class PaymentsApiClient : BaseHttpClient, IPaymentsApiClient
    {
        public const string PAYMENTS_API_HOST = "PAYMENTS_API_HOST";

        private readonly string _paymentsRoute;

        public PaymentsApiClient(HttpClient httpClient, IConfiguration configuration) : base(httpClient)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _paymentsRoute = $"{configuration[PAYMENTS_API_HOST]}";
        }

        public async Task<HttpResponseMessage> CreatePayment(PaymentRequest payload)
        {
            return await PutAsync($"{_paymentsRoute}/api/payments", payload);
        }

        public async Task<HttpResponseMessage> GetPayments(int merchantId)
        {
            return await GetAsync($"{_paymentsRoute}/api/payments?merchantId={merchantId}");
        }
    }
}