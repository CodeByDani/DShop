using Microsoft.AspNetCore.Mvc;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Category.GetCategory;

public sealed partial class GetCategory
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/category/{id:long}", async ([FromRoute] long id, ISender sender) =>
                {
                    var resQuery = await sender.Send(new ReqQuery { Id = id });
                    if (resQuery.IsError)
                    {
                        return Results.BadRequest(resQuery.Errors);
                    }

                    var result = resQuery.Adapt<GetCategoryEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get Category")
                .WithTags("Category")
                .Produces(StatusCodes.Status200OK, typeof(GetCategoryEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Category For DShop")
                .WithDescription("For Getting Category Should Use This API!");
        }
    }
}