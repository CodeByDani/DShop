using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product", async (CreateEndPointRequest request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }
                    var res = resCommand.Value.Adapt<CreateEndPointResponse>();
                    return Results.Created($"/product/{res.Id}", res);
                })
                .WithName("Create Product")
                .Produces(StatusCodes.Status201Created, typeof(CreateEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product For DShop")
                .WithDescription("For Creating Product Should Use This API!");
        }
    }
}