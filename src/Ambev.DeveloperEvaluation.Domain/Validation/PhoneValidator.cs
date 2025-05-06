using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(data => data)
            .NotEmpty().WithMessage("The phone cannot be empty.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must start with '+' followed by 11-15 digits.");
    }
}
