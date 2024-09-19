using Microsoft.AspNetCore.Mvc;
using Results = Microsoft.AspNetCore.Http.Results;

namespace Catalog.API.Features.Category.DeleteCategory;


public sealed partial class DeleteCategory
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/category/{id}", async ([FromRoute] long id, ISender sender) =>
                {
                    var resCommand = await sender.Send(new ReqCommand { Id = id });
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest((object)resCommand.Errors);
                    }
                    return Results.NoContent();
                })
                .WithName("Delete Category")
                .WithTags("Category")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Category For DShop")
                .WithDescription("For Deleting Category Should Use This API!");
        }
    }
}