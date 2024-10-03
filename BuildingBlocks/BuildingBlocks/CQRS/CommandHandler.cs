using BuildingBlocks.Base;
using BuildingBlocks.Behavior;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public abstract class BaseCommandHandler<TCommand, TResponse> : BaseRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : ResponseBaseService, new()
{
}
