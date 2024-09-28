using ErrorOr;
using System.Reflection;

namespace BuildingBlocks.Behavior;

public static class ErrorOrExtensions
{
    public static TResponse ToTResponse<TResponse>(this Error error)
    {
        var errors = new List<Error> { error };

        return errors.ToTResponse<TResponse>();
    }

    public static TResponse ToTResponse<TResponse>(this List<Error> errors)
    {
        var response = (TResponse)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response;
    }
}