using Basket.API.Entities;
using BuildingBlocks.Base;

namespace Basket.API.Feature.Basket.StoreBasket;

public sealed class StoreBasketReqCommand : ICommand<StoreBasketResCommand>
{
    public ShoppingCart Cart { get; set; }
}


public sealed class StoreBasketResCommand : ResponseBaseService
{
    public string State { get; set; }
}