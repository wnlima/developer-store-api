using System.Runtime.CompilerServices;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(DefaultContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProductEntity>> GetProducts(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
    }
}