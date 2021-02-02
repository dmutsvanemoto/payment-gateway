using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentsGateway.Models.BankAPI;

namespace PaymentsGateway.HttpClients
{
    public class BankHttpClient : BaseHttpClient, IBankHttpClient
    {
        public BankHttpClient(HttpClient httpClient) : base(httpClient)
        {

        }
        
        public Task<HttpResponseMessage> Authenticate(string clientId, string clientSecret)
        {
            if (clientId == "client_id" && clientSecret == "client_secret")
            {
                return Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            }

            return Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });
        }
        
        public async Task<HttpResponseMessage> ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var validator = new ProcessPaymentRequestValidator();
            var validationResults = await validator.ValidateAsync(processPaymentRequest);

            if (validationResults.IsValid)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
        }
    }
}