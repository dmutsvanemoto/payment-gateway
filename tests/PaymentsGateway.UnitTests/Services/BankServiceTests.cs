using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentsGateway.HttpClients;
using PaymentsGateway.Services;
using Xunit;

namespace PaymentsGateway.UnitTests.Services
{
    public class BankServiceTests
    {
        [Theory]
        [MemberData(nameof(ConstructorMemberData))]
        public void Constructor_Throws(IConfiguration configuration, IMapper mapper, IBankHttpClient bankHttpClient, string name)
        {
            Action act = () => { new BankService(configuration, mapper, bankHttpClient); };
            act.Should().Throw<ArgumentNullException>()
                .Where(x => x.Message.Contains(name));
        }

        public static TheoryData<IConfiguration, IMapper, IBankHttpClient, string> ConstructorMemberData =>
            new TheoryData<IConfiguration, IMapper, IBankHttpClient, string>
            {
                { null, Mock.Of<IMapper>(), Mock.Of<IBankHttpClient>(), "configuration"},
                { Mock.Of<IConfiguration>(), null, Mock.Of<IBankHttpClient>(), "mapper"},
                { Mock.Of<IConfiguration>(), Mock.Of<IMapper>(), null, "bankHttpClient"}
            };
    }
}
