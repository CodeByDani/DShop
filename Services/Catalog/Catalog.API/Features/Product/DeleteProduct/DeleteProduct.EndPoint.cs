using Microsoft.AspNetCore.Mvc;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.DeleteProduct;

public sealed partial class DeleteProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id:long}", async ([FromRoute] long id, ISender sender) =>
                {
                    var resCommand = await sender.Send(new ReqCommand { Id = id });
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }
                    return Results.NoContent();
                })
                .WithName("Delete Product")
                .WithTags("Product")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product For DShop")
                .WithDescription("For Deleting Product Should Use This API!");
        }
    }
}