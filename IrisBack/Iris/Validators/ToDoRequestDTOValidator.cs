using FluentValidation;
using IrisCore.DTOs;

namespace Iris.Validators
{
    public class ToDoRequestDTOValidator : AbstractValidator<TaskRequestDTO>
    {
        public ToDoRequestDTOValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(250).WithMessage("Description cannot be longer than 250 characters.");

            RuleFor(x => x.IsFavorite)
                .NotNull().WithMessage("Is Favorite is required.");

            RuleFor(x => x.IsCompleted)
                .NotNull().WithMessage("Is Completed is required.");
        }
    }
}


