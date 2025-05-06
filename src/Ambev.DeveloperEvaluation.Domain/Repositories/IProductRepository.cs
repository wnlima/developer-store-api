using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository : IGenericRepository<ProductEntity>
{
    Task<IEnumerable<ProductEntity>> GetProducts(Guid[] ids, CancellationToken cancellationToken = default);
}
