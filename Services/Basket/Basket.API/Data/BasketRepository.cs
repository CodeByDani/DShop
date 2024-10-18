using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;

namespace Basket.API.Data;

public class BasketRepository : BaseRepository<ShoppingCart>, IBasketRepository
{
    private readonly IMongoCollection<ShoppingCart> _collection;
    public BasketRepository(IMongoCollection<ShoppingCart> mongoCollection) : base(mongoCollection)
    {
        _collection = mongoCollection;
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