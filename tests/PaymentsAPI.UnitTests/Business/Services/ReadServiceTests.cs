using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using PaymentsAPI.Business.Services;
using PaymentsAPI.Data;
using PaymentsAPI.Data.Models;
using Xunit;

namespace PaymentsAPI.UnitTests.Business.Services
{
    public class ReadServiceTests
    {
        public IList<Payment> Payments { get; set; }

        [Fact]
        public async Task GetByMerchantId_When_MerchantId_Has_No_Payments_Return_Empty_List()
        {
            var mockContext = new Mock<PaymentsDbContext>();
            mockContext.Setup(x => x.Payments).ReturnsDbSet(Payments);
            
            var service = new ReadService(mockContext.Object);

            var payments = await service.GetByMerchantId(123);

            payments.Should().HaveCount(0);
        }


        [Fact]
        public async Task GetByMerchantId_When_MerchantId_Has_Payments_Return_Payments_List()
        {
            var payment = new Payment
            {
                PaymentId = 321,
                MerchantId = 123,
                Amount = 234.23m,
                Currency = "USD"
            };

            Payments.Add(payment);

            var mockContext = new Mock<PaymentsDbContext>();
            mockContext.Setup(x => x.Payments).ReturnsDbSet(Payments);


            var service = new ReadService(mockContext.Object);

            var payments = await service.GetByMerchantId(123);

            payments.Should().HaveCount(1);
        }
    }
}
