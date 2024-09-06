namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/product", async (CreateProduct request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    var res = await sender.Send(req);
                    return Results.Created($"/product/{res.Id}", res);
                })
                .WithName("Create Product")
                .Produces(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product For DShop")
                .WithDescription("For Creating Product Should Use This API!");
            ;
        }
    }
}