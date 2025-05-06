using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

public class ListProductsRequest : AbstractAdvancedFilter
{
    public ListProductsRequest(Dictionary<string, string>? filters)
    {
        Filters = filters;
        ResolveFields();
    }
}
