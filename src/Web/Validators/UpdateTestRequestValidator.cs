using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class UpdateTestRequestValidator : AbstractValidator<UpdateTestRequest>
    {
        public UpdateTestRequestValidator()
        {
            RuleFor(updateTestRequest => updateTestRequest.Text)
                .MinimumLength(3)
                .MaximumLength(10)
                .NotNull();
        }
    }
}