using Catalog.API.Common.Interfaces;
using System.Linq.Expressions;

namespace Catalog.API.Common;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IDocumentSession _documentSession;
    private readonly IQuerySession _querySession;

    public BaseRepository(IDocumentSession documentSession, IQuerySession querySession)
    {
        _documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        _querySession = querySession ?? throw new ArgumentNullException(nameof(querySession));
    }

    public async Task Store(TEntity entity, CancellationToken cancellationToken)
    {
        _documentSession.Store(entity);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(TEntity entity, CancellationToken cancellationToken)
    {
        _documentSession.Update(entity);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }
    public async Task Delete(TEntity entity, CancellationToken cancellationToken)
    {
        _documentSession.Delete(entity);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteById(object id, CancellationToken cancellationToken)
    {
        _documentSession.Delete(id);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _querySession.Query<TEntity>().ToListAsync(cancellationToken);
    }
    public async Task<TResult> FindFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default)
    {
        return await _querySession.Query<TEntity>()
            .Where(predicate)
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _querySession.Query<TEntity>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteRange(List<TEntity> entities, CancellationToken cancellationToken)
    {
        _documentSession.DeleteObjects(entities);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<TResult>, int TotalCount)> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy,
        bool isDescending,
        CancellationToken cancellationToken)
    {
        var query = _querySession.Query<TEntity>().Where(predicate);

        if (orderBy != null)
        {
            query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
        }

        var items = await query
            .Select(selector)
            .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        var count = await query.CountAsync(cancellationToken);

        return (items, count);

    }
}