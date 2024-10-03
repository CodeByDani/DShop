using BuildingBlocks.Base;
using BuildingBlocks.Behavior;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
where TResponse : ResponseBaseService, new()
{

}