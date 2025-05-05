using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : GenericRepository<SaleItemEntity>, ISaleItemRepository
{
    public SaleItemRepository(DefaultContext context) : base(context)
    {
    }

    public override IQueryable<SaleItemEntity> ApplyUserIdFilter(IQueryable<SaleItemEntity> query)
    {
        if (_customerId != Guid.Empty)
            query = query.Where(o => o.CustomerId == _customerId);

        return query;
    }
}
