using Basket.API.Entities;
using BuildingBlocks.Base;

namespace Basket.API.Feature.Basket.GetBasket;

public sealed class GetBasketResQuery : IQuery<GetBasketReqQuery>
{
    public string UserName { get; set; }
}

public sealed class GetBasketReqQuery : ResponseBaseService
{
    public ShoppingCart Cart { get; set; }
}