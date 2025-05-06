namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class CreateSaleItemRequest
{
    public Guid CustomerId { get; set; } = Guid.Empty;
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}