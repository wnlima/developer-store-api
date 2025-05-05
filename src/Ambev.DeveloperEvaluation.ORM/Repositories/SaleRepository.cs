using System.Diagnostics.Metrics;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : GenericRepository<SaleEntity>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {
    }

    public override IQueryable<SaleEntity> ApplyUserIdFilter(IQueryable<SaleEntity> query)
    {
        if (_customerId != Guid.Empty)
            query = query.Where(o => o.CustomerId == _customerId);

        return query;
    }

    public async Task Cancel(Guid saleId, CancellationToken cancellationToken = default)
    {
        var saleToUpdate = new SaleEntity { Id = saleId, IsCancelled = true };
        _context.Sales.Attach(saleToUpdate);
        _context.Entry(saleToUpdate).Property(p => p.IsCancelled).IsModified = true;
        await this.SaveAsync(cancellationToken);
    }

    public async Task<SaleEntity?> GetDetailsAsync(Guid saleId, CancellationToken cancellationToken = default, bool track = false)
    {
        if (track)
            return await _context.Sales
                .Where(s => s.Id == saleId)
                .Include(s => s.Customer)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                .FirstOrDefaultAsync(cancellationToken);
        else
            return await _context.Sales.AsNoTracking()
            .Where(s => s.Id == saleId)
            .Include(s => s.Customer)
            .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
            .FirstOrDefaultAsync(cancellationToken);
    }
}