namespace Basket.API.Feature.Basket.GetBasket;

public sealed class GetBasketEndPointModel : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketReqQuery
                {
                    UserName = username
                });
                if (result.IsError) return Results.NotFound(result.Errors);
                return Results.Ok(result);
            }).WithName("Get Basket")
            .WithTags("Basket")
            .Produces(StatusCodes.Status200OK, typeof(GetBasketEndPointReq))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket For DShop")
            .WithDescription("For Creating Basket Should Use This API!");
    }
}