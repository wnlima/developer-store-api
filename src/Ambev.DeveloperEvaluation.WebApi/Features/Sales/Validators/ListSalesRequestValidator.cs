using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;

public class ListSalesRequestValidator : AbstractAdvancedFilterValidator<SaleResponse, ListSalesRequest>
{
    public ListSalesRequestValidator() : base()
    {
        RuleFor(x => x.CustomerId)
        .NotEmpty().WithMessage("Customer is required.");
    }
}