using BuildingBlocks.Behavior;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.Base;

public abstract class BaseRequestHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
    where TResponse : ResponseBaseService, new()
{
    internal BaseRequestHandler() { }

    public Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return HandleCore(request, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCore(TCommand request, CancellationToken cancellationToken);
    protected TResponse Failure(Error error)
    {
        return new TResponse { Errors = new List<Error> { error } };
    }
}
