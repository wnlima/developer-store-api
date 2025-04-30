using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<ProductEntity> CreateAsync(ProductEntity product);
    Task<ProductEntity?> GetByIdAsync(Guid id);
    Task<IPaginatedList<ProductEntity>> ListAsync(int pageNumber, int pageSize);
    Task UpdateAsync(ProductEntity product);
    Task DeleteAsync(Guid id);
}
