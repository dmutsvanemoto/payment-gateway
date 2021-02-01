using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using PaymentsAPI.Business.Services;
using PaymentsAPI.Data;
using PaymentsAPI.Data.Models;
using PaymentsAPI.Data.Models.Requests;
using Xunit;

namespace PaymentsAPI.UnitTests.Business.Services
{
    public class WriteServiceTests
    {
        public IList<Payment> Payments { get; set; }
        
        //[Fact]TODO: Fix test
        public async Task CreatePayment_When_CreatePaymentRequest_IsValid_Return()
        {
            var payment = new CreatePaymentRequest()
            {
                MerchantId = 123,
                Amount = 234.23m,
                Currency = "USD"
            };

            var mockContext = new Mock<PaymentsDbContext>();
            mockContext.Setup(x => x.Payments)
                .ReturnsDbSet(Payments);

            mockContext.SetupGet(x => x.Payments);
            
            var service = new WriteService(mockContext.Object);

            await service.CreatePayment(payment);

            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
