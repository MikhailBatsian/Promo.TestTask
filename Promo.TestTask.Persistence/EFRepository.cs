using Microsoft.EntityFrameworkCore;
using Promo.TestTask.Core;

namespace Promo.TestTask.Persistence;
public abstract class EFRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    protected EFRepository(DbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public IQueryable<T> GetQuery(bool useTracking = false)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (!useTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}
