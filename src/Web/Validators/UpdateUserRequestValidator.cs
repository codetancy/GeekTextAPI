using FluentValidation;
using Web.Contracts.V1.Requests;
using Web.Constants;

namespace Web.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(uur => uur.PhoneNumber)
                .Matches(RegularExpressions.PhoneRegex)
                .WithMessage("'Phone' must be in the format +1 123-456-789 (area code is optional).");
        }
    }
}
