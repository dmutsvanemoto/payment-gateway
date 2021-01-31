using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.Controllers;
using PaymentsAPI.Data.Models.Requests;
using Xunit;

namespace PaymentsAPI.UnitTests.Controllers
{
    public class WriteControllerTests
    {
        [Fact]
        public async Task GetPaymentsByMerchantId_Returns_InternalServerError_By_Default()
        {
            var request = new CreatePaymentRequest()
            {
                Amount = 12.34m,
                Currency = "USD",
                MerchantId = 123
            };
            var controller = new WriteController();

            var expected = new StatusCodeResult(500);
            var actual = await controller.CreatePayment(request);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
