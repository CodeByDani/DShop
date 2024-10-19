using Basket.API.Entities;

namespace Basket.API.Feature.Basket.StoreBasket;

public sealed class StoreBasketEndPointReq
{
    public ShoppingCart Cart { get; set; }
}