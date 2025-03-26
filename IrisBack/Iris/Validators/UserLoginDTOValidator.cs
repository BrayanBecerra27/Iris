using FluentValidation;
using IrisCore.DTOs;

namespace Iris.Validators
{
    public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.");
        }
    }
}


