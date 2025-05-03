using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

namespace Ambev.DeveloperEvaluation.Application.Products.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(p => p.Description)
            .MaximumLength(1000);

        RuleFor(p => p.Price)
            .GreaterThan(0);

        RuleFor(p => p.QuantityInStock)
            .GreaterThanOrEqualTo(0);
    }
}
