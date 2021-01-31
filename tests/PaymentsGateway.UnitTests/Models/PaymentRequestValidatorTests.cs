using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;
using PaymentsGateway.Models;
using Xunit;

namespace PaymentsGateway.UnitTests.Models
{
    public class PaymentRequestValidatorTests
    {
        [Fact]
        public void Should_Not_Have_Validation_Error_For_Valid_CardNumber()
        {
            var validator = new PaymentRequestValidator();

            validator.ShouldNotHaveValidationErrorFor(request => request.CardNumber, "4242424242424242");
        }

        [Theory]
        [MemberData(nameof(InvalidCreditCardNumberData))]
        public void Should_Have_Validation_Error_For_Invalid_CardNumber(string cardNumber)
        {
            var validator = new PaymentRequestValidator();
            
            validator.ShouldHaveValidationErrorFor(request => request.CardNumber, cardNumber);
        }


        [Fact]
        public void Should_Not_Have_Validation_Error_For_Current_ExpiryMonth_And_ExpiryYear()
        {
            var validator = new PaymentRequestValidator();

            var paymentRequest = new PaymentRequest
            {
                CardNumber = "4242424242424242",
                Amount = 32.56m,
                Currency = "USD",
                Cvv = "123",

                ExpiryMonth = DateTime.Now.Month,
                ExpiryYear = DateTime.Now.Year
            };

            var validationResult = validator.Validate(paymentRequest);

            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().NotContain(x => x.PropertyName == "ExpiryMonth");
            validationResult.Errors.Should().NotContain(x => x.PropertyName == "ExpiryYear");
        }

        // TODO: Add Test Coverage for expiry dates.

        [Fact]
        public void Should_Have_Validation_Error_For_Invalid_Amount()
        {
            var validator = new PaymentRequestValidator();

            validator.ShouldHaveValidationErrorFor(request => request.Amount, -0.003m);
            validator.ShouldHaveValidationErrorFor(request => request.Amount, -1);
        }


        [Fact]
        public void Should_Have_Validation_Error_For_Invalid_Currency()
        {
            var validator = new PaymentRequestValidator();

            validator.ShouldHaveValidationErrorFor(request => request.Currency, null as string);
            validator.ShouldHaveValidationErrorFor(request => request.Currency, "");
            validator.ShouldHaveValidationErrorFor(request => request.Currency, "abc1");
        }

        [Fact]
        public void Should_Have_Validation_Error_For_Invalid_Cvv()
        {
            var validator = new PaymentRequestValidator();

            validator.ShouldHaveValidationErrorFor(request => request.Cvv, null as string);
            validator.ShouldHaveValidationErrorFor(request => request.Cvv, "");
            validator.ShouldHaveValidationErrorFor(request => request.Cvv, "abc1");
        }


        public static TheoryData<string> InvalidCreditCardNumberData =>
            new()
            {
                { null as string },
                { "" },
                { "1234" },
                { "4242424.24242424" },
                { "424242424242424ab" },
                { "42424242424242423" }
            };
    }
}
