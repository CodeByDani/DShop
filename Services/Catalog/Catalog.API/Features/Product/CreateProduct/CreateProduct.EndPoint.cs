using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product", async (EndPointRequest request, ISender sender) =>
                {
                    await Task.CompletedTask;
                    var req = request.Adapt<ReqCommand>();
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }
                    var res = resCommand.Value.Adapt<EndPointResponse>();
                    return Results.Created($"/product/{res.Id}", res);
                })
                .WithName("Create Product")
                .Produces(StatusCodes.Status201Created, typeof(EndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product For DShop")
                .WithDescription("For Creating Product Should Use This API!");
        }
    }
}