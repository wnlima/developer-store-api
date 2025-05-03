namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
}