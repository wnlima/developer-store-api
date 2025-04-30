using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleItemRepository
{
    Task<SaleItemEntity> CreateAsync(SaleItemEntity saleItem);
    Task<SaleItemEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(SaleItemEntity saleItem);
    Task DeleteAsync(Guid id);
}
