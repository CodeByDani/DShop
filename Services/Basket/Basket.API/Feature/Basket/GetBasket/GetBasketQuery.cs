using Basket.API.Entities;
using BuildingBlocks.Base;

namespace Basket.API.Feature.Basket.GetBasket;

public sealed class GetBasketReqQuery : IQuery<GetBasketResQuery>
{
    public string UserName { get; set; }
}

public sealed class GetBasketResQuery : ResponseBaseService
{
    public ShoppingCart Cart { get; set; }
}