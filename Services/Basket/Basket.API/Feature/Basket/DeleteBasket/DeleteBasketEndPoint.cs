namespace Basket.API.Feature.Basket.DeleteBasket;

public sealed class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketReqCommand
                {
                    UserName = username
                });
                if (result.IsError) return Results.NotFound(result.Errors);
                return Results.NoContent();
            }).WithName("Delete Basket")
            .WithTags("Basket")
            .Produces(StatusCodes.Status200OK, typeof(DeleteBasketResCommand))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket For DShop")
            .WithDescription("For Creating Basket Should Use This API!");
    }
}