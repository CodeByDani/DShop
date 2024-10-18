using Basket.API.Entities;

namespace Basket.API.Feature.Basket.GetBasket;

public class GetBasketQueryHandler : BaseQueryHandler<GetBasketResQuery, GetBasketReqQuery>
{
    protected override async Task<GetBasketReqQuery> HandleCore(GetBasketResQuery request,
        CancellationToken cancellationToken)
    {
        return new GetBasketReqQuery
        {
            Cart = new ShoppingCart("swa")
        };
    }
}