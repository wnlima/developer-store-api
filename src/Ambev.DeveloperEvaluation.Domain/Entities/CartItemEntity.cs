using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartItemEntity : BaseUserIdentityEntity
{
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public CartEntity Cart { get; set; }
    public ProductEntity Product { get; set; }
    public UserEntity Customer { get; set; }
}
