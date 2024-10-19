using Basket.API.Data.Interfaces;
using Basket.API.Entities;

namespace Basket.API.Feature.Basket.GetBasket;

public class GetBasketQueryHandler : BaseQueryHandler<GetBasketReqQuery, GetBasketResQuery>
{
    private readonly IBasketRepository _repository;

    public GetBasketQueryHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<GetBasketResQuery> HandleCore(GetBasketReqQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetBasketAsync(request.UserName, cancellationToken);
        if (result == null) return Failure(Error.NotFound("Error", "Error"));
        return new GetBasketResQuery
        {
            Cart = result
        };
    }
}