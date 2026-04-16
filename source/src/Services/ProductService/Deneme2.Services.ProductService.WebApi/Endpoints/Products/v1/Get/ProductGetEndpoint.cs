using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.ProductService.Application.Products.v1.Models;
using Deneme2.Services.ProductService.Application.Products.v1.Queries.Get;

namespace Deneme2.Services.ProductService.WebApi.Endpoints.Products.v1.Get;

public sealed class ProductGetEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Products)
            .RequireAuthorization();

        routeGroup.MapGet("{productId:guid}", GetProduct)
            .Produces<ProductViewModel>()
            .ProducesProblem()
            .WithDescription("Get product by id")
            .WithName(nameof(GetProduct));
    }

    private static Task<IResult> GetProduct(Guid productId, ISender sender, CancellationToken cancellationToken = default) =>
            sender
               .Send(new GetProductQuery(productId), cancellationToken)
               .Match(
                     product => TypedResults.Ok(product),
                     errors => errors.ToProblemResult(),
                     cancellationToken);
}
