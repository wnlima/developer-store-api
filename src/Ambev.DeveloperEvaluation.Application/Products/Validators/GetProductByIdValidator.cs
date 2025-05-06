using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.Commands;

namespace Ambev.DeveloperEvaluation.Application.Products.Validators;

public class GetProductByIdValidator : AbstractValidator<GetProductByIdCommand>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}