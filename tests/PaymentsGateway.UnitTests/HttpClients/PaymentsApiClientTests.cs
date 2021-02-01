using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using PaymentsGateway.Extensions;
using PaymentsGateway.HttpClients;
using PaymentsGateway.Models;
using Xunit;

namespace PaymentsGateway.UnitTests.HttpClients
{
    public class PaymentsApiClientTests
    {
        private readonly IConfiguration _configuration;
        public PaymentsApiClientTests()
        {
            _configuration = Mock.Of<IConfiguration>(m => m[It.Is<string>(o => o == PaymentsApiClient.PAYMENTS_API_HOST)] == Url);
        }

        private const string Url = "https://localhost:8080";

        [Theory]
        [InlineData(null)]
        private void Constructor_Throws(IConfiguration configuration)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            Action act = () => { new PaymentsApiClient(httpClient, configuration); };

            act.Should().Throw<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{nameof(configuration)}')");
        }


        [Theory]
        [InlineData(HttpStatusCode.NoContent)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        public async Task CreatePayment_Expected_StatusCode_Is_Return(HttpStatusCode statusCode)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var absoluteURi = $"{Url}/api/payments";
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri == absoluteURi && x.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var paymentsApiClient = new PaymentsApiClient(httpClient, _configuration);


            var response = await paymentsApiClient.CreatePayment(new PaymentRequest());


            response.StatusCode.Should().Be(statusCode);
        }

        [Theory]
        [MemberData(nameof(GetPaymentsTheoryData))]
        public async Task GetPayments_Expected_StatusCode_Is_Return(HttpStatusCode statusCode, HttpContent content)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var merchantId = 123;
            var absoluteURi = $"{Url}/api/payments?merchantId={merchantId}";
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri == absoluteURi && x.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var paymentsApiClient = new PaymentsApiClient(httpClient, _configuration);


            var response = await paymentsApiClient.GetPayments(123);


            response.StatusCode.Should().Be(statusCode);
        }


        public static TheoryData<HttpStatusCode, HttpContent> GetPaymentsTheoryData
        {
            get
            {
                var data = new TheoryData<HttpStatusCode, HttpContent>();

                data.Add(HttpStatusCode.OK, new PaymentsContract()
                {
                    PaymentId = 123, 
                    Amount = 456.99m,
                    Currency = "USD"
                }.ToJsonStringContent());

                data.Add(HttpStatusCode.BadRequest, new StringContent(""));
                data.Add(HttpStatusCode.InternalServerError, new StringContent(""));

                return data;
            }
        }
    }
}
