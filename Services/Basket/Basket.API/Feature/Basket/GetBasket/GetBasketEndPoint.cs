namespace Basket.API.Feature.Basket.GetBasket;

public sealed class GetBasketEndPointModel : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = sender.Send(new GetBasketResQuery
                {
                    UserName = username
                });
            }).WithName("Get Basket")
            .WithTags("Basket")
            .Produces(StatusCodes.Status200OK, typeof(GetBasketEndPointReq))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket For DShop")
            .WithDescription("For Creating Basket Should Use This API!");
    }
}