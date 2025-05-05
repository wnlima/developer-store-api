using Ambev.DeveloperEvaluation.WebApi.Features.Products.DTOs;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DTOs;

public class SaleItemResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public ProductResponse Product { get; set; }
    public SaleResponse Sale { get; set; }
    public UserResponse Customer { get; set; }
}