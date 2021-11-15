using System;
using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        public CreateCardRequestValidator()
        {
            RuleFor(ccr => ccr.CardNumber)
                .CreditCard()
                .NotEmpty();

            RuleFor(ccr => ccr.ExpirationMonth)
                .Matches("(0[1-9])|(1[0-2])")
                .WithMessage("'Expiration Month' must be in MM format.");

            RuleFor(ccr => ccr.ExpirationYear)
                .Length(4)
                .Matches("[0-9]*")
                .WithMessage("'Expiration Year' must be in YYYY format.")
                .Must(p => int.Parse(p) >= DateTime.Now.Year)
                .WithMessage("Card expired.");

            RuleFor(ccr => ccr.SecurityCode)
                .Length(3)
                .Matches("[0-9]*")
                .WithMessage("'Security Code' must be in XXX format.");
        }
    }
}
