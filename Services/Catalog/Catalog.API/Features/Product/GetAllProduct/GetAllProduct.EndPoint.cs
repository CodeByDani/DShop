﻿using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.GetAllProduct;

public sealed partial class GetAllProduct
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetAllEndPointRequest request, ISender sender) =>
                {
                    var query = request.Adapt<ReqQuery>();
                    var resQuery = await sender.Send(query);
                    if (resQuery.IsError)
                    {
                        return Results.BadRequest(resQuery.Errors);
                    }

                    var result = resQuery.Adapt<GetAllEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get All Product")
                .Produces(StatusCodes.Status200OK, typeof(GetAllEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Product For DShop")
                .WithDescription("For Getting All Product Should Use This API!");
        }
    }
}