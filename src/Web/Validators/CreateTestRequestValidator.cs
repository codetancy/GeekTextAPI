using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class CreateTestRequestValidator : AbstractValidator<CreateTestRequest>
    {
        public CreateTestRequestValidator()
        {
            RuleFor(createTestRequest => createTestRequest.Text)
                .MinimumLength(3)
                .MaximumLength(10)
                .NotNull();
        }
    }
}