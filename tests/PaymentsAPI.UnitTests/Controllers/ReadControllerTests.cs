using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Business.Services;
using PaymentsAPI.Controllers;
using PaymentsAPI.Data.Models;
using Xunit;

namespace PaymentsAPI.UnitTests.Controllers
{
    public class ReadControllerTests
    {
        private readonly Mock<IReadService> _mockReadService;
        private readonly ILogger<ReadController> _logger;

        public ReadControllerTests()
        {
            _mockReadService = new Mock<IReadService>();
            _logger = Mock.Of<ILogger<ReadController>>();
        }

        [Theory]
        [MemberData(nameof(ConstructorMemberData))]
        public void Constructor_Throws(ILogger<ReadController> logger, IReadService readService, string name)
        {
            Action act = () => { new ReadController(logger, readService); };
            act.Should().Throw<ArgumentNullException>()
                .Where(x => x.Message.Contains(name));
        }

        public static TheoryData<ILogger<ReadController>, IReadService, string> ConstructorMemberData =>
            new TheoryData<ILogger<ReadController>, IReadService, string>
            {
                { null, Mock.Of<IReadService>(), "logger"},
                { Mock.Of<ILogger<ReadController>>(), null, "readService"}
            };

        [Fact]
        public async Task GetPaymentsByMerchantId_When_Exception_Is_Thrown_Then_Return_InternalServerError()
        {
            var merchantId = 123123;
            var controller = new ReadController(_logger, _mockReadService.Object);

            _mockReadService.Setup(x => x.GetByMerchantId(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var expected = new StatusCodeResult(500);
            var actual = await controller.GetPaymentsByMerchantId(merchantId);

            actual.Should().BeEquivalentTo(expected);
            _mockReadService.Verify();
        }

        [Fact]
        public async Task GetPaymentsByMerchantId_When_MerchantId_Is_Null_Then_Return_BadRequest()
        {
            var controller = new ReadController(_logger, _mockReadService.Object);

            var expected = new BadRequestResult();
            var actual = await controller.GetPaymentsByMerchantId(null);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPaymentsByMerchantId_When_MerchantId_Has_Zero_Then_Return_Ok_With_Empty_List()
        {
            var merchantId = 123;
            var payments = new List<Payment>();

            _mockReadService
                .Setup(x => x.GetByMerchantId(merchantId))
                .ReturnsAsync(payments);

            var controller = new ReadController(_logger, _mockReadService.Object);

            var expected = new OkObjectResult(payments);
            var actual = await controller.GetPaymentsByMerchantId(merchantId);

            actual.Should().BeEquivalentTo(expected);
            _mockReadService.Verify();
        }

        [Fact]
        public async Task GetPaymentsByMerchantId_When_MerchantId_Has_Payments_Then_Return_Ok_With_Payment_List()
        {
            var merchantId = 123;
            var payments = new List<Payment>
            {
                new Payment() { PaymentId = 321, MerchantId = merchantId, Amount = 432.23m, Currency = "USD" }
            };

            _mockReadService
                .Setup(x => x.GetByMerchantId(merchantId))
                .ReturnsAsync(payments);

            var controller = new ReadController(_logger, _mockReadService.Object);

            var expected = new OkObjectResult(payments);
            var actual = await controller.GetPaymentsByMerchantId(merchantId);

            actual.Should().BeEquivalentTo(expected);
            _mockReadService.Verify();
        }
    }
}

