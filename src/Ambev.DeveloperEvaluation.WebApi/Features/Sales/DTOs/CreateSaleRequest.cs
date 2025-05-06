namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class CreateSaleRequest
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<CreateSaleItemRequest> SaleItems { get; set; }
}