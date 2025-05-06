using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository : IGenericRepository<SaleEntity>
{
    Task<SaleEntity?> GetDetailsAsync(Guid saleId, CancellationToken cancellationToken = default, bool track = false);
}