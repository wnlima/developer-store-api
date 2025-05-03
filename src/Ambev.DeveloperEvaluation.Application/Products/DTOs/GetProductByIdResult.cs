namespace Ambev.DeveloperEvaluation.Application.Products.DTOs;

public class GetProductByIdResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
}
