using Ambev.DeveloperEvaluation.Application.Products.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Application.Products.Validators;

public class ListProductsCommandValidator : AbstractAdvancedFilterValidator<ProductEntity, ListProductsCommand>
{
}