using BuildingBlocks;

namespace Basket.API.Feature.Basket.StoreBasket;

public class StoreBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Basket", async (StoreBasketEndPointReq model, ISender sender) =>
            {
                var result = await sender.Send(new StoreBasketReqCommand { Cart = model.Cart });
                if (result.IsError) return result.ToHttpError();
                return Results.Created("", result);
            }).WithName("Store Basket")
            .WithTags("Basket")
            .Produces(StatusCodes.Status201Created, typeof(StoreBasketEndPointReq))
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket For DShop")
            .WithDescription("For Creating Basket Should Use This API!"); ;
    }
}
