using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<ProductEntity> CreateAsync(ProductEntity product);
    Task<ProductEntity?> GetByIdAsync(Guid id);
    Task<PaginatedList<ProductEntity>> ListAsync(AbstractAdvancedFilter filter);
    Task UpdateAsync(ProductEntity product);
    Task<bool> DeleteAsync(Guid id);
}
