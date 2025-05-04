using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(DefaultContext context) : base(context)
    {
    }
}