using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class SaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
    public decimal TotalDiscounts { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public UserResponse Customer { get; set; }
    public ICollection<SaleItemResponse> SaleItems { get; set; } = new List<SaleItemResponse>();
}
