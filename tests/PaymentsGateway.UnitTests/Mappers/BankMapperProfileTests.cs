using AutoMapper;
using PaymentsGateway.Mappers;
using Xunit;

namespace PaymentsGateway.UnitTests.Mappers
{
    public class BankMapperProfileTests
    {
        [Fact]
        public void AssertConfiguration()
        {
            var configuration = new MapperConfiguration(x => x.AddProfile<BankMapperProfile>());
            configuration.AssertConfigurationIsValid();
        }
    }
}
