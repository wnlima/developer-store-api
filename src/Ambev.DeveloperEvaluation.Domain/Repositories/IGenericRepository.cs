using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class, new()
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default,bool track = false);
    Task<PaginatedList<TEntity>> ListAsync(AbstractAdvancedFilter filter, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
