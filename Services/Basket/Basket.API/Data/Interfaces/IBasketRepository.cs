using Basket.API.Entities;
using BuildingBlocks.DependencyInjection;

namespace Basket.API.Data.Interfaces;

public interface IBasketRepository : IRepository<ShoppingCart>, IScopeLifetime
{
    Task<ShoppingCart> GetBasketAsync(string username, CancellationToken cancellation);
    Task<string> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellation);
    Task<bool> DeleteBasketAsync(string username, CancellationToken cancellation);
}