using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class DiscountService : IDiscountService
{
    public Task Apply(SaleItemEntity entity)
    {
        entity.Discount = 0m;

        if (entity.IsCancelled)
        {
            entity.CalculateTotalAmount();
            return Task.CompletedTask;
        }

        if (entity.Quantity >= 4 && entity.Quantity < 10)
        {
            entity.Discount = 0.10m;
        }
        else if (entity.Quantity >= 10 && entity.Quantity <= 20)
        {
            entity.Discount = 0.20m;
        }

        entity.CalculateTotalAmount();
        return Task.CompletedTask;
    }
}