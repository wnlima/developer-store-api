using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class ListSalesCommandValidator : AbstractAdvancedFilterValidator<SaleResult, ListSalesCommand>
{
    public ListSalesCommandValidator() : base()
    {
        RuleFor(x => x.CustomerId)
        .NotEmpty().WithMessage("Customer is required.");
    }
}

public class ManagerListSalesCommandValidator : AbstractAdvancedFilterValidator<SaleResult, ListSalesCommand> { }