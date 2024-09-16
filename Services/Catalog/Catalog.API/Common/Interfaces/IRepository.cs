using System.Linq.Expressions;

namespace Catalog.API.Common.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task Store(TEntity entity, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteById(object id, CancellationToken cancellationToken = default);
    Task DeleteRange(List<TEntity> entities, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<TResult> FindFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<TResult>, int TotalCount)> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<TResult>, int TotalCount)> GetWithPagination<TResult>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default);
}