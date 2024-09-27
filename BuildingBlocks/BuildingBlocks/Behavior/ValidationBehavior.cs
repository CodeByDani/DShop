using BuildingBlocks.CQRS;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behavior;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, ErrorOr<TResponse>>
where TRequest : ICommand<ErrorOr<TResponse>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(
            v => v.ValidateAsync(context, cancellationToken)));
        var validationErrors = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(e => e.Errors)
            .Select(e => $"{e.ErrorCode}: {e.ErrorMessage}")
            .ToList();
        if (validationErrors.Any())
        {
            var errorMessages = string.Join("\n", validationErrors);
            return Error.Validation("ValidationError", errorMessages);
        }

        return await next();
    }
}