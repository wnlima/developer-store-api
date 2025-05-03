namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

public class GetProductByIdRequest
{
    public Guid Id { get; set; }

    public GetProductByIdRequest(Guid id)
    {
        Id = id;
    }
}
