
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Validators;

public class ListProductsRequestValidator : AbstractAdvancedFilterValidator<ProductEntity, ListProductsRequest>
{

}