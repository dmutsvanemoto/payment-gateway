using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsAPI.Business.Services;
using PaymentsAPI.Controllers;
using PaymentsAPI.Data.Models.Requests;
using Xunit;

namespace PaymentsAPI.UnitTests.Controllers
{
    public class WriteControllerTests
    {
        private readonly ILogger<WriteController> _logger;
        private readonly Mock<IWriteService> _mockWriteService;

        public WriteControllerTests()
        {
            _logger = Mock.Of<ILogger<WriteController>>();
            _mockWriteService = new Mock<IWriteService>();
        }

        [Theory]
        [MemberData(nameof(ConstructorMemberData))]
        public void Constructor_Throws(ILogger<WriteController> logger, IWriteService writeService, string name)
        {
            Action act = () => { new WriteController(logger, writeService); };
            act.Should().Throw<ArgumentNullException>()
                .Where(x => x.Message.Contains(name));
        }

        public static TheoryData<ILogger<WriteController>, IWriteService, string> ConstructorMemberData =>
            new TheoryData<ILogger<WriteController>, IWriteService, string>
            {
                { null, Mock.Of<IWriteService>(), "logger"},
                { Mock.Of<ILogger<WriteController>>(), null, "writeService"}
            };

        [Fact]
        public async Task CreatePayment_When_Exception_Is_Thrown_Then_Return_InternalServerError()
        {
            var request = new CreatePaymentRequest()
            {
                Amount = 12.34m,
                Currency = "USD",
                MerchantId = 123
            };
            var controller = new WriteController(_logger, _mockWriteService.Object);

            _mockWriteService.Setup(x => x.CreatePayment(It.IsAny<CreatePaymentRequest>()))
                .ThrowsAsync(new Exception());

            var expected = new StatusCodeResult(500);
            var actual = await controller.CreatePayment(request);

            actual.Should().BeEquivalentTo(expected);
            _mockWriteService.Verify();
        }

        [Fact]
        public async Task CreatePayment_When_Payload_Is_Invalid_Then_Return_BadRequest()
        {
            var request = new CreatePaymentRequest()
            {
                Amount = 12.34m,
                Currency = "USD",
                MerchantId = 123
            };

            var controller = new WriteController(_logger, _mockWriteService.Object);

            controller.ModelState.AddModelError("CreatePaymentRequest", "Invalid payload request!");

            var expected = new BadRequestResult();
            var actual = await controller.CreatePayment(request);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task CreatePayment_When_Payload_Is_Valid_Then_Return_NoContent()
        {
            var request = new CreatePaymentRequest()
            {
                Amount = 12.34m,
                Currency = "USD",
                MerchantId = 123
            };

            var controller = new WriteController(_logger, _mockWriteService.Object);
            
            var expected = new NoContentResult();
            var actual = await controller.CreatePayment(request);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
