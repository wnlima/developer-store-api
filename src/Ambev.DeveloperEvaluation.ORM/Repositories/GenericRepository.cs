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

    public GenericRepository(DefaultContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool track = false)
    {
        if (track)
            return await _context.Set<TEntity>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        else
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public virtual async Task<PaginatedList<TEntity>> ListAsync(AbstractAdvancedFilter filter, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().CreatePaginatedListAsync(filter, cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}