using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItemEntity : BaseUserIdentityEntity
{
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public ProductEntity Product { get; set; }
    public UserEntity Customer { get; set; }
    public void CalculateTotalAmount()
    {
        TotalAmount = Quantity * UnitPrice * (1 - Discount);
    }
}
