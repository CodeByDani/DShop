﻿using Basket.API.Data.Interfaces;
using Basket.API.Feature.Basket.Common;

namespace Basket.API.Feature.Basket.StoreBasket;

public sealed class StoreBasketCommandHandler : BaseCommandHandler<StoreBasketReqCommand, StoreBasketResCommand>
{
    private readonly IBasketRepository _repository;

    public StoreBasketCommandHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<StoreBasketResCommand> HandleCore(StoreBasketReqCommand request, CancellationToken cancellationToken)
    {
        var response = await _repository.StoreBasketAsync(request.Cart, cancellationToken);
        if (response == null) return Failure(Error.Conflict(nameof(BasketMessage.ExistBasket), BasketMessage.ExistBasket));
        return new StoreBasketResCommand
        {
            State = response
        };
    }
}