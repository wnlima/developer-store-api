namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs;

public class GetSaleByIdResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
}
