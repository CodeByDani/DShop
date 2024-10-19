using Basket.API.Data.Interfaces;

namespace Basket.API.Feature.Basket.DeleteBasket;

public class DeleteBasketCommandHandler : BaseCommandHandler<DeleteBasketReqCommand, DeleteBasketResCommand>
{
    private readonly IBasketRepository _repository;

    public DeleteBasketCommandHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    protected override async Task<DeleteBasketResCommand> HandleCore(DeleteBasketReqCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteBasketAsync(request.UserName, cancellationToken);
        if (!result) return Failure(Error.Failure("error", "error"));
        return new DeleteBasketResCommand();
    }
}