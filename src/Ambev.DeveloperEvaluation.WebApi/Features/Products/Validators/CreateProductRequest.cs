using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Validators;

public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
