using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using PaymentsGateway.HttpClients;
using PaymentsGateway.Models;
using PaymentsGateway.Models.BankAPI;

namespace PaymentsGateway.Services
{
    public class BankService : IBankService
    {
        private const string CLIENT_ID = "BANK_CLIENT_ID";
        private const string CLIENT_SECRET = "BANK_CLIENT_SECRET";

        private string ClientId { get; }
        private string ClientSecret { get; }

        private readonly IMapper _mapper;
        private readonly IBankHttpClient _bankHttpClient;
        
        public BankService(IConfiguration configuration, IMapper mapper, IBankHttpClient bankHttpClient)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bankHttpClient = bankHttpClient ?? throw new ArgumentNullException(nameof(bankHttpClient));
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            ClientId = $"{configuration[CLIENT_ID]}";
            ClientSecret = $"{configuration[CLIENT_SECRET]}";
        }

        public async Task ProcessPayment(PaymentRequest payload)
        {
            var processPaymentRequest = _mapper.Map<ProcessPaymentRequest>(payload);

            var authenticateResponse = await _bankHttpClient.Authenticate(ClientId, ClientSecret);

            authenticateResponse.EnsureSuccessStatusCode();

            var processPaymentResponse = await _bankHttpClient.ProcessPayment(processPaymentRequest);

            processPaymentResponse.EnsureSuccessStatusCode();
        }
    }
}