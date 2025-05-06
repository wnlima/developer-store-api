using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(username => username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");
    }
}