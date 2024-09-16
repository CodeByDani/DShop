using Microsoft.AspNetCore.Mvc;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async ([FromRoute] long id, ISender sender) =>
                {
                    var resCommand = await sender.Send(new ReqCommand { Id = id });
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest(resCommand.Errors);
                    }

                    var result = resCommand.Adapt<GetEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get Product")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product For DShop")
                .WithDescription("For Getting Product Should Use This API!");
        }
    }
}