using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Controllers;
using Xunit;

namespace PaymentsAPI.UnitTests.Controllers
{
    public class ReadControllerTests
    {
        [Fact]
        public async Task GetPaymentsByMerchantId_Returns_InternalServerError_By_Default()
        {
            var merchantId = 0;
            var controller = new ReadController();

            var expected = new StatusCodeResult(500);
            var actual = await controller.GetPaymentsByMerchantId(merchantId);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
