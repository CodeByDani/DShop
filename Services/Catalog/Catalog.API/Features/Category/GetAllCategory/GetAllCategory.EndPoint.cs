using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Category.GetAllCategory;

public sealed partial class GetAllCategory
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories", async (ISender sender) =>
                {
                    var resQuery = await sender.Send(new ReqQuery());
                    if (resQuery.IsError)
                    {
                        return Results.BadRequest(resQuery.Errors);
                    }

                    var result = resQuery.Adapt<GetAllCategoryEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get All Category")
                .WithTags("Category")
                .Produces(StatusCodes.Status200OK, typeof(GetAllCategoryEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Category For DShop")
                .WithDescription("For Getting All Category Should Use This API!");
        }
    }
}