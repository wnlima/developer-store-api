using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Validators;

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}