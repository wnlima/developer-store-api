namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

public class DeleteProductRequest
{
    public Guid Id { get; set; }

    public DeleteProductRequest(Guid id)
    {
        Id = id;
    }
}
