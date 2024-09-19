using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Category.CreateCategory;

public sealed partial class CreateCategory
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/category", async (CreateCategoryEndPointRequest request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest((object)resCommand.Errors);
                    }
                    var res = resCommand.Value.Adapt<CreateCategoryEndPointResponse>();
                    return Results.Created($"/category/{res.Id}", (object)res);
                })
                .WithName("Create Category")
                .WithTags("Category")
                .Produces(StatusCodes.Status201Created, typeof(CreateCategoryEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Category For DShop")
                .WithDescription("For Creating Category Should Use This API!");
        }
    }
}