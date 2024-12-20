using System.Linq.Expressions;
using System.Text.Json;
using Basket.API.Data.Interfaces;
using Basket.API.DependencyInjection;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Basket.API.Data
{
    public class CachedBasketRepository<TEntity> :BaseRepository<ShoppingCart>,IBasketRepository
    {
        private readonly IBasketRepository _repository;
        private readonly IDistributedCache _distributedCache;
        private readonly IMongoCollection<ShoppingCart> _collection;

        public CachedBasketRepository(IMongoClient client, IOptions<MongoDbSettings> settings,
            IBasketRepository repository, IDistributedCache distributedCache) : base(client, settings)
        {
            _repository = repository;
            _distributedCache = distributedCache;
            _collection = GetCollection();
        }

        public async Task<bool> DeleteBasketAsync(string username, CancellationToken cancellation)
        {
            var result = await _repository.DeleteBasketAsync(username, cancellation);
            await _distributedCache.RemoveAsync(username, cancellation);
            return result;
        }

       
        public async Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellation)
        {
           var cachedBasket = await _distributedCache.GetStringAsync(username, cancellation);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            var basket = await _repository.GetBasketAsync(username, cancellation);
            if (basket != null)
                await _distributedCache.SetStringAsync(username,JsonSerializer.Serialize(basket), cancellation);
            return basket;
        } 

        public async Task<string> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellation)
        {
           var result = await _repository.StoreBasketAsync(basket, cancellation);
            await _distributedCache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket),cancellation);
            return result;
        }
    }
}
