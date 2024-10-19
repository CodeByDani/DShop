using BuildingBlocks.Base;

namespace Basket.API.Feature.Basket.DeleteBasket;

public sealed class DeleteBasketReqCommand : ICommand<DeleteBasketResCommand>
{
    public string UserName { get; set; }
}

public sealed class DeleteBasketResCommand : ResponseBaseService
{

}