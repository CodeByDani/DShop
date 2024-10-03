using BuildingBlocks.Base;
using BuildingBlocks.Behavior;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public abstract class BaseQueryHandler<TQuery, TResponse> : BaseRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : ResponseBaseService, new()
{

}