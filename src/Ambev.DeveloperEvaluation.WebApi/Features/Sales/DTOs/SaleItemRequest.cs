namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class SaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsCancelled { get; set; }
}