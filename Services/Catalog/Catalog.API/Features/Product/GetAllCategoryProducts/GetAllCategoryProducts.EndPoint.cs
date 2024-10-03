using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Product.GetAllCategoryProducts;

public sealed partial class GetAllCategoryProducts
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{Id:long}/products", async (long id, [AsParameters] GetAllCategoryProductsEndPointRequest request, ISender sender) =>
                {
                    var query = request.Adapt<ReqQuery>();
                    query.CategoryId = id;
                    var resQuery = await sender.Send(query);
                    if (resQuery.IsError)
                    {
                        return Results.BadRequest(resQuery.Errors);
                    }

                    var result = resQuery.Adapt<GetAllCategoryProductsEndPointResponse>();
                    return Results.Ok(result);
                })
                .WithName("Get All Category Products")
                .WithTags("Product")
                .Produces(StatusCodes.Status200OK, typeof(GetAllCategoryProductsEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Category Products For DShop")
                .WithDescription("For Getting All Category Products Should Use This API!");
        }
    }
}