using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using PaymentsGateway.HttpClients;
using PaymentsGateway.Models;

namespace PaymentsGateway.Services
{
    public class PaymentService : IPaymentsService
    {
        private readonly IPaymentsApiClient _paymentsApiClient;

        public PaymentService(IPaymentsApiClient paymentsApiClient)
        {
            _paymentsApiClient = paymentsApiClient;
        }

        public async Task<bool> ProcessPayment(PaymentRequest payload)
        {
            var response = await _paymentsApiClient.CreatePayment(payload);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IList<PaymentsContract>> RetrievePayments(int merchantId)
        {
            var response = await _paymentsApiClient.GetPayments(merchantId);

            if (response.IsSuccessStatusCode)
            {
                var payloadString = await response.Content.ReadAsStringAsync();
                var payload = JsonSerializer.Deserialize<IList<PaymentsContract>>(payloadString);

                return payload;
            }

            return null;
        }
    }
}