using BuildingBlocks.Base;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behavior;

public sealed class ValidationBehavior<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBaseService, new()
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    protected override async Task<TResponse> HandleCore(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(error => Error.Validation(
                code: error.PropertyName,
                description: error.ErrorMessage));

        return new TResponse
        {
            Errors = errors
        };
    }
}