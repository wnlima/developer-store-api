using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class ListSalesCommandValidator : AbstractAdvancedFilterValidator<ProductEntity, ListSalesCommand>
{
}