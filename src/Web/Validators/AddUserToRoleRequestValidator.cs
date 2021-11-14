using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class AddUserToRoleRequestValidator : AbstractValidator<AddUserToRoleRequest>
    {
        public AddUserToRoleRequestValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(32);
        }
    }
}
