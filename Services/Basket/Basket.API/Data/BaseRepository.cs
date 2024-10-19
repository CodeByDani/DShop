using Basket.API.Data.Interfaces;
using Basket.API.DependencyInjection;
using Basket.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Basket.API.Data;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _mongoCollection;

    public BaseRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _mongoCollection = database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    protected IMongoCollection<TEntity> GetCollection()
    {
        return _mongoCollection;
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _mongoCollection.InsertOneAsync(entity, null, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
        await _mongoCollection
            .ReplaceOneAsync(filter, entity, new ReplaceOptions(), cancellationToken);
    }
    public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
        await _mongoCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {
        var ids = entities.Select(e => e.Id).ToList();
        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        await _mongoCollection.DeleteManyAsync(filter, cancellationToken);
    }
    public async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _mongoCollection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _mongoCollection
            .Find(predicate)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult> FindFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken)
    {
        return await _mongoCollection
            .Find(predicate)
            .Project(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _mongoCollection
            .Find(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<TResult>, long TotalCount)> FindWithPagination<TResult>(Expression<Func<TEntity, bool>> predicate,
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>> orderBy,
        bool isDescending,
        CancellationToken cancellationToken)
    {
        var query = _mongoCollection.Find(predicate);

        if (orderBy != null)
        {
            var sortDefinition = isDescending
                ? Builders<TEntity>.Sort.Descending(orderBy)
                : Builders<TEntity>.Sort.Ascending(orderBy);
            query.Sort(sortDefinition);
        }

        var items = await query
            .Project(selector)
            .Skip(pageIndex * pageSize).Limit(pageSize).ToListAsync(cancellationToken);
        var count = await query.CountDocumentsAsync(cancellationToken);

        return (items, count);

    }

    public async Task<(IReadOnlyList<TResult>, long TotalCount)> GetWithPagination<TResult>(int pageIndex, int pageSize, Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, object>> orderBy = null,
        bool isDescending = false, CancellationToken cancellationToken = default)
    {
        var query = _mongoCollection.Find(FilterDefinition<TEntity>.Empty);
        if (orderBy != null)
        {
            var sortDefinition = isDescending
                ? Builders<TEntity>.Sort.Descending(orderBy)
                : Builders<TEntity>.Sort.Ascending(orderBy);
            query.Sort(sortDefinition);
        }

        var items = await query
            .Project(selector)
            .Skip(pageIndex * pageSize).Limit(pageSize).ToListAsync(cancellationToken);
        var count = await query.CountDocumentsAsync(cancellationToken);

        return (items, count);
    }
}