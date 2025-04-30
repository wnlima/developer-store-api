using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartEntity : BaseUserIdentityEntity
{
    public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public UserEntity Customer { get; set; }
}