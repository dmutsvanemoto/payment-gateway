using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using PaymentsGateway.HttpClients;
using PaymentsGateway.Models;
using PaymentsGateway.Services;
using Xunit;

namespace PaymentsGateway.UnitTests.Services
{
    public class PaymentsServiceTests
    {
        private readonly Mock<IPaymentsApiClient> _paymentsApiClient;

        public PaymentsServiceTests()
        {
            _paymentsApiClient = new Mock<IPaymentsApiClient>();
        }

        [Fact]
        public async Task ProcessPayment_When_PaymentsAPI_returns_NoContent_Then_Return_True()
        {
            _paymentsApiClient.Setup(x => x.CreatePayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NoContent
                });

            var service = new PaymentService(_paymentsApiClient.Object);

            var payload = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var actual = await service.ProcessPayment(payload);

            actual.Should().BeTrue();
            _paymentsApiClient.Verify();
        }


        [Fact]
        public async Task ProcessPayment_When_PaymentsAPI_returns_InternalServerError_Then_Return_False()
        {
            _paymentsApiClient.Setup(x => x.CreatePayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent(JsonSerializer.Serialize(new { message = "Something Went Wrong!" }), Encoding.UTF8, "application/json")
                });

            var service = new PaymentService(_paymentsApiClient.Object);

            var payload = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var actual = await service.ProcessPayment(payload);

            actual.Should().BeFalse();
            _paymentsApiClient.Verify();
        }

        [Fact]
        public async Task ProcessPayment_When_PaymentsAPI_returns_BadRequest_Then_Return_False()
        {
            _paymentsApiClient.Setup(x => x.CreatePayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                });

            var service = new PaymentService(_paymentsApiClient.Object);

            var payload = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var actual = await service.ProcessPayment(payload);

            actual.Should().BeFalse();
            _paymentsApiClient.Verify();
        }
    }
}
