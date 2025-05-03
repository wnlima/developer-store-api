using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<SaleEntity> CreateAsync(SaleEntity sale);
    Task<SaleEntity?> GetByIdAsync(Guid id);
    Task<PaginatedList<SaleEntity>> ListAsync(int pageNumber, int pageSize);
    Task UpdateAsync(SaleEntity sale);
    Task DeleteAsync(Guid id);
}
