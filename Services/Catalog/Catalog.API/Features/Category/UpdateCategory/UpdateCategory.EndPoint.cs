﻿using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
{
    public sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/category/{id:long}", async ([FromRoute] long id
                    , [FromBody] UpdateCategoryEndPointRequest request, ISender sender) =>
                {
                    var req = request.Adapt<ReqCommand>();
                    req.CategoryId = id;
                    var resCommand = await sender.Send(req);
                    if (resCommand.IsError)
                    {
                        return Results.BadRequest((object)resCommand.Errors);
                    }
                    var res = resCommand.Value.Adapt<UpdateCategoryEndPointResponse>();
                    return Results.Created($"/category/{res.Id}", (object)res);
                })
                .WithName("Update Category")
                .WithTags("Category")
                .Produces(StatusCodes.Status201Created, typeof(UpdateCategoryEndPointResponse))
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Category For DShop")
                .WithDescription("For Updating Category Should Use This API!");
        }
    }
}