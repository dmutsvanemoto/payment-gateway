using System;
using FluentValidation;

namespace PaymentsGateway.Models
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .CreditCard();

            RuleFor(x => x.ExpiryMonth)
                .GreaterThanOrEqualTo(DateTime.Now.Month)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.ExpiryYear)
                .GreaterThanOrEqualTo(DateTime.Now.Year);

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0.000m);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(3);

            RuleFor(x => x.Cvv)
                .NotEmpty()
                .Length(3);
        }
    }
}