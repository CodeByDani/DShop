using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product/{id}", async ([FromRoute] long id
                    , [FromBody] UpdateEndPointRequest request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    req.ProductId = id;
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }
                    var res = resCommand.Value.Adapt<UpdateEndPointResponse>();
                    return Results.Created($"/product/{res.Id}", res);
                })
                .WithName("Update Product")
                .Produces(StatusCodes.Status201Created, typeof(UpdateEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product For DShop")
                .WithDescription("For Updating Product Should Use This API!");
        }
    }
}