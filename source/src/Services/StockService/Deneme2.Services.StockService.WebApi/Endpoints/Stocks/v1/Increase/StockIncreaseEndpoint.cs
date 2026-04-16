using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.StockService.Application.Stocks.v1.Commands.Increase;
using Microsoft.AspNetCore.Mvc;

namespace Deneme2.Services.StockService.WebApi.Endpoints.Stocks.v1.Increase;

public sealed class StockIncreaseEndpoint : CarterModule
{
    public sealed record IncreaseStockRequest(int Amount)
    {
        public IncreaseStockCommand ToCommand(Guid productId) => new(productId, Amount);
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Stocks)
            .RequireAuthorization();

        routeGroup.MapPatch("{productId:guid}/increase", IncreaseStock)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem()
            .WithDescription("Increase stock quantity for a product")
            .WithName(nameof(IncreaseStock));
    }

    private static Task<IResult> IncreaseStock(
        Guid productId,
        [FromBody] IncreaseStockRequest request,
        ISender sender,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(request.ToCommand(productId), cancellationToken)
            .Match(
                () => TypedResults.NoContent(),
                errors => errors.ToProblemResult(),
                cancellationToken);
}
