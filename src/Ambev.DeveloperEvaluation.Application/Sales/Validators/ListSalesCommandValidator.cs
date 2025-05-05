using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class ListSalesCommandValidator : AbstractAdvancedFilterValidator<ProductEntity, ListSalesCommand>
{
    public ListSalesCommandValidator() : base()
    {
        RuleFor(x => x.CustomerId)
        .NotEmpty().WithMessage("Customer is required.");
    }
}

public class ManagerListSalesCommandValidator : AbstractAdvancedFilterValidator<ProductEntity, ListSalesCommand> { }