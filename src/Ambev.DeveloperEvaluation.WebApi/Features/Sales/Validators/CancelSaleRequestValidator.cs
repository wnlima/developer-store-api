using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Validators;

public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    public CancelSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}