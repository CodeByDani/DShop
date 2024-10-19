using Basket.API.Data.Interfaces;
using Basket.API.DependencyInjection;
using Basket.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Basket.API.Data;

public class BasketRepository : BaseRepository<ShoppingCart>, IBasketRepository
{
    private readonly IMongoCollection<ShoppingCart> _collection;

    public BasketRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
        : base(client, settings)
    {
        _collection = GetCollection();
        UsernameIndex();
    }

    private void UsernameIndex()
    {
        var indexKeysDefinition = Builders<ShoppingCart>
            .IndexKeys.Ascending(p => p.UserName);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<ShoppingCart>(indexKeysDefinition, indexOptions);
        _collection.Indexes.CreateOne(indexModel);
    }

    public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellation)
        => await _collection.Find(p => p.UserName == username)
            .FirstOrDefaultAsync(cancellation);


    public async Task<string> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellation)
    {
        var result = await _collection
            .ReplaceOneAsync(p => p.UserName == basket.UserName,
                basket,
                new ReplaceOptions { IsUpsert = true },
                cancellation);
        if (!result.IsAcknowledged) return null;
        return result.ModifiedCount != 0 ? "Modified" : "Inserted";
    }

    public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellation)
    {
        var result = await _collection
            .DeleteOneAsync(p => p.UserName == username, cancellation);
        return result.DeletedCount != 0;
    }

}