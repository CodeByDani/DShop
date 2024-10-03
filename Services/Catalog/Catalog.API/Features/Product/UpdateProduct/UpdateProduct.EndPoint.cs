using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product/{id:long}", async ([FromRoute] long id
                    , [FromBody] UpdateProductEndPointRequest request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    req.ProductId = id;
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }
                    var res = resCommand.Adapt<UpdateProductEndPointResponse>();
                    return Results.Created($"/product/{res.Id}", res);
                })
                .WithName("Update Product")
                .WithTags("Product")
                .Produces(StatusCodes.Status201Created, typeof(UpdateProductEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product For DShop")
                .WithDescription("For Updating Product Should Use This API!");
        }
    }
}