using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.StockService.Application.Stocks.v1.Commands.Decrease;
using Microsoft.AspNetCore.Mvc;

namespace Deneme2.Services.StockService.WebApi.Endpoints.Stocks.v1.Decrease;

public sealed class StockDecreaseEndpoint : CarterModule
{
    public sealed record DecreaseStockRequest(int Amount)
    {
        public DecreaseStockCommand ToCommand(Guid productId) => new(productId, Amount);
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Stocks)
            .RequireAuthorization();

        routeGroup.MapPatch("{productId:guid}/decrease", DecreaseStock)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem()
            .WithDescription("Decrease stock quantity for a product")
            .WithName(nameof(DecreaseStock));
    }

    private static Task<IResult> DecreaseStock(
        Guid productId,
        [FromBody] DecreaseStockRequest request,
        ISender sender,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(request.ToCommand(productId), cancellationToken)
            .Match(
                () => TypedResults.NoContent(),
                errors => errors.ToProblemResult(),
                cancellationToken);
}
