using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentsGateway.Controllers;
using PaymentsGateway.Models;
using PaymentsGateway.Services;
using Xunit;

namespace PaymentsGateway.UnitTests.Controllers
{
    public class PaymentsControllerTests
    {
        private readonly Mock<IPaymentsService> _mockPaymentsService;
        public PaymentsControllerTests()
        {
            _mockPaymentsService = new Mock<IPaymentsService>();
        }

        [Fact]
        public async Task When_Payload_Is_Invalid_Then_Return_BadRequestResult()
        {
            _mockPaymentsService.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(false);

            var controller = new PaymentsController(_mockPaymentsService.Object);

            var paymentRequest = new PaymentRequest
            {
                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var validator = new PaymentRequestValidator();
            var validationResult = await validator.ValidateAsync(paymentRequest);
            validationResult.AddToModelState(controller.ModelState, null);
            
            var expected = new BadRequestResult();
            var actual = await controller.ProcessPayment(paymentRequest);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task When_PaymentsService_Fails_Then_Return_BadRequestResult()
        {
            _mockPaymentsService.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(false);

            var controller = new PaymentsController(_mockPaymentsService.Object);

            var paymentRequest = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var expected = new BadRequestResult();
            var actual = await controller.ProcessPayment(paymentRequest);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task When_Payment_Is_Process_Then_Return_NoContentResult()
        {
            _mockPaymentsService.Setup(x => x.ProcessPayment(It.IsAny<PaymentRequest>()))
                .ReturnsAsync(true);

            var controller = new PaymentsController(_mockPaymentsService.Object);

            var paymentRequest = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var expected = new NoContentResult();
            var actual = await controller.ProcessPayment(paymentRequest);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task When_MerchantId_Is_Invalid_Then_Return_BadRequest()
        {
            var merchantId = 0;
            var controller = new PaymentsController(_mockPaymentsService.Object);

            var expected = new BadRequestResult();
            var actual = await controller.RetrievePayments(merchantId);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task When_MerchantId_Is_Valid_And_No_Payments_Then_Return_Ok()
        {
            var merchantId = 5600;
            var payments = new List<PaymentsContract>();
            
            _mockPaymentsService.Setup(x => x.RetrievePayments(It.IsAny<int>()))
                .ReturnsAsync(payments);

            var controller = new PaymentsController(_mockPaymentsService.Object);

            var expected = new OkObjectResult(payments);
            var actual = await controller.RetrievePayments(merchantId);

            actual.Should().BeEquivalentTo(expected);
            _mockPaymentsService.Verify();
        }

        [Fact]
        public async Task When_MerchantId_Is_Valid_Then_Return_Ok()
        {
            var merchantId = 5600;
            var payment = new PaymentsContract {PaymentId = 1738, Amount = 134.86m, Currency = "USD"};
            var payments = new List<PaymentsContract> {payment};

            _mockPaymentsService.Setup(x => x.RetrievePayments(It.IsAny<int>()))
                .ReturnsAsync(payments);

            var controller = new PaymentsController(_mockPaymentsService.Object);

            var expected = new OkObjectResult(payments);
            var actual = await controller.RetrievePayments(merchantId);

            actual.Should().BeEquivalentTo(expected);
            _mockPaymentsService.Verify();
        }
    }
}