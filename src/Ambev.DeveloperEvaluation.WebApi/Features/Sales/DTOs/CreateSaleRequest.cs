namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class CreateSaleRequest
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<SaleItemRequest> SaleItems { get; set; } = new List<SaleItemRequest>();
}