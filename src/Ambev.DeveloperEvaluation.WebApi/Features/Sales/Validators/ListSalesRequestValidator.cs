using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;

public class ListSalesRequestValidator : AbstractAdvancedFilterValidator<ProductEntity, ListSalesRequest>
{
}