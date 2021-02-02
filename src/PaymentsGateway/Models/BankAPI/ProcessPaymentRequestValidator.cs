using System.Collections.Generic;
using FluentValidation;

namespace PaymentsGateway.Models.BankAPI
{
    public class ProcessPaymentRequestValidator : AbstractValidator<ProcessPaymentRequest>
    {
        private static IList<int> WhitelistedMerchantIds = new List<int>() {123, 456, 789};
        private static IList<string> WhitelistedCurrencies = new List<string>() {"USD", "GBP"};

        private static string ApprovedCardNumber = "4242424242424242";

        public ProcessPaymentRequestValidator()
        {
            RuleFor(x => x.MerchantId)
                .Must(merchantId => WhitelistedMerchantIds.Contains(merchantId))
                .WithMessage("Unrecognised {PropertyName}");

            RuleFor(x => x.Currency)
                .Must(currency => WhitelistedCurrencies.Contains(currency))
                .WithMessage("Unrecognised {PropertyName}");

            RuleFor(x => x.CardDetails)
                .NotNull();

            When(x => x.CardDetails != null, () =>
            {
                RuleFor(x => x.CardDetails.CardNumber)
                    .Must(cardNumber => ApprovedCardNumber == cardNumber);

                //TODO -> simulate card expiry verification

                RuleFor(x => x.CardDetails.Cvv)
                    .Length(3);
            });


        }
    }
}