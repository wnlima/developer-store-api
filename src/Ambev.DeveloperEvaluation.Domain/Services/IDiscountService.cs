using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IDiscountService
{
    Task Apply(SaleItemEntity entity);
}