using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(c => c.RoleName)
                .NotNull()
                .NotEmpty()
                .Must(c => !c.Contains(" ")).WithMessage("Role name cannot contain whitespace.")
                .MaximumLength(23);
        }
    }
}
