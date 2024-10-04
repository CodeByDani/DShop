using BuildingBlocks.Base;
using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks;

public static class ResultExtensions
{
    public static IResult ToHttpError(this ResponseBaseService service)
    {
        if (!service.IsError) throw new ArgumentException();
        var error = service.Errors.First();

        return error.Type switch
        {
            ErrorType.NotFound => Results.NotFound(error),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Validation => Results.BadRequest(service.Errors),
            ErrorType.Conflict => Results.Conflict(error),
            ErrorType.Forbidden => Results.Forbid(),
            ErrorType.Failure => Results.BadRequest(error),
            ErrorType.Unexpected => Results.BadRequest(error),
            _ => Results.Problem("An unknown error occurred.")
        };
    }
}
