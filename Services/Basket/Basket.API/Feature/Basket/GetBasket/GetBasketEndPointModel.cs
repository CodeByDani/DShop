using Basket.API.Entities;

namespace Basket.API.Feature.Basket.GetBasket;

public sealed class GetBasketEndPointReq
{
    public ShoppingCart Cart { get; set; }
}