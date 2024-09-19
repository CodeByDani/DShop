using Microsoft.AspNetCore.Mvc;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.GetProduct;

public sealed partial class GetProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{id:long}", async ([FromRoute] long id, ISender sender) =>
                {
                    var resQuery = await sender.Send(new ReqQuery { Id = id });
                    if (resQuery.IsError)
                    {
                        return Results.BadRequest(resQuery.Errors);
                    }

                    var result = resQuery.Value.Adapt<GetProductEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get Product")
                .WithTags("Product")
                .Produces(StatusCodes.Status200OK, typeof(GetProductEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product For DShop")
                .WithDescription("For Getting Product Should Use This API!");
        }
    }
}