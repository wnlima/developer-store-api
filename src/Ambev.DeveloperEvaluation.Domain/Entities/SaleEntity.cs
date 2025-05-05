using System.Runtime.CompilerServices;
using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleEntity : BaseUserIdentityEntity
{
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
    public decimal TotalDiscounts { get; set; }
    public string Branch { get; set; }
    public bool IsCancelled { get; set; }
    public UserEntity Customer { get; set; }
    public ICollection<SaleItemEntity> SaleItems { get; set; } = new List<SaleItemEntity>();

    public void Compute()
    {
        TotalAmount = SaleItems.Sum(item => item.TotalAmount);
        TotalDiscounts = SaleItems.Sum(item => item.Discount * item.UnitPrice * item.Quantity);
        TotalItems = SaleItems.Sum(item => item.Quantity);
    }
}