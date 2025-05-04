using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

public class GetProductByIdRequest : IIdentifier
{
    public Guid Id { get; set; }

    public GetProductByIdRequest(Guid id)
    {
        Id = id;
    }
}
