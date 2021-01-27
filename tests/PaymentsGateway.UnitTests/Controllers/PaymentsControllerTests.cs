using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PaymentsGateway.Controllers;
using Xunit;

namespace PaymentsGateway.UnitTests.Controllers
{
    public class PaymentsControllerTests
    {
        [Fact]
        public async Task Should_Return_BadRequestResult()
        {
            var controller = new PaymentsController();

            var expected = new BadRequestResult();
            var actual = await controller.ProcessPayment();

            actual.Should().BeEquivalentTo(expected);
        }
    }
}