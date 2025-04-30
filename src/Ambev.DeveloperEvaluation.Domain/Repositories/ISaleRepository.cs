using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<SaleEntity> CreateAsync(SaleEntity sale);
    Task<SaleEntity?> GetByIdAsync(Guid id);
    Task<IPaginatedList<SaleEntity>> ListAsync(int pageNumber, int pageSize);
    Task UpdateAsync(SaleEntity sale);
    Task DeleteAsync(Guid id);
}
