using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IIdentifier, new()
{
    protected readonly DefaultContext _context;
    protected Guid _customerId = Guid.Empty;

    public GenericRepository(DefaultContext context)
    {
        _context = context;
    }

    public void SetCurrentUser(Guid customerId)
    {
        _customerId = customerId;
    }

    public virtual IQueryable<TEntity> ApplyUserIdFilter(IQueryable<TEntity> query)
    {
        return query;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await this.SaveAsync(cancellationToken);
        return entity;
    }

    public IQueryable<TEntity> GetQueryable(bool track)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (!track)
            query = query.AsNoTracking();

        if (_customerId != Guid.Empty)
            query = ApplyUserIdFilter(query);

        return query;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool track = false)
    {
        return await GetQueryable(track).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public virtual async Task<PaginatedList<TEntity>> ListAsync(AbstractAdvancedFilter filter, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(false).CreatePaginatedListAsync(filter, cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
        await this.SaveAsync(cancellationToken);
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken, true);

        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await this.SaveAsync(cancellationToken);
            return true;
        }

        return false;
    }

    public virtual async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
        _context.ChangeTracker.Clear();
    }
}