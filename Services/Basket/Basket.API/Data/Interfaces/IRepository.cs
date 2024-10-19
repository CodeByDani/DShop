using Basket.API.Entities;
using System.Linq.Expressions;

namespace Basket.API.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
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

    Task<(IReadOnlyList<TResult>, long TotalCount)> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<TResult>, long TotalCount)> GetWithPagination<TResult>(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false,
        CancellationToken cancellationToken = default);
}