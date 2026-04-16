using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.StockService.Application.Stocks.v1.Models;
using Deneme2.Services.StockService.Application.Stocks.v1.Queries.GetByProductId;

namespace Deneme2.Services.StockService.WebApi.Endpoints.Stocks.v1.Get;

public sealed class StockGetByProductIdEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Stocks)
            .AllowAnonymous();

        routeGroup.MapGet("{productId:guid}", GetStock)
            .Produces<StockViewModel>()
            .ProducesProblem()
            .WithDescription("Get stock by product id")
            .WithName(nameof(GetStock));
    }

    private static Task<IResult> GetStock(Guid productId, ISender sender, CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetStockByProductIdQuery(productId), cancellationToken)
            .Match(
                stock => TypedResults.Ok(stock),
                errors => errors.ToProblemResult(),
                cancellationToken);
}
