using ErrorOr;
using MediatR;

namespace BuildingBlocks.Base;

public abstract class BasePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBaseService, new()
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return HandleCore(request, next, cancellationToken);
    }

    protected abstract Task<TResponse> HandleCore(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);

    protected TResponse Failure(List<Error> errors)
    {
        return new TResponse { Errors = errors };
    }

    protected TResponse Failure(Error error)
    {
        return new TResponse { Errors = new List<Error> { error } };
    }

}