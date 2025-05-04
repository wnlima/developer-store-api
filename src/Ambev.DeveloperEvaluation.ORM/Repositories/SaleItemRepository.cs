using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : GenericRepository<SaleItemEntity>, ISaleItemRepository
{
    public SaleItemRepository(DefaultContext context) : base(context)
    {
    }
}
