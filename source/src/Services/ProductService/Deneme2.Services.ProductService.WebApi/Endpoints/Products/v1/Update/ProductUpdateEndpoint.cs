using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.ProductService.Application.Products.v1.Commands.Update;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Microsoft.AspNetCore.Mvc;

namespace Deneme2.Services.ProductService.WebApi.Endpoints.Products.v1.Update;

public sealed class ProductUpdateEndpoint : CarterModule
{
    public sealed record ProductUpdateRequest(
        string? Name,
        string? Description,
        decimal Price,
        Currency Currency)
    {
        public ProductUpdateCommand ToCommand(Guid productId) =>
            new(productId, Name, Description, Price, Currency);
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Products)
            .RequireAuthorization();

        routeGroup.MapPut("{productId:guid}", UpdateProduct)
            .Produces(HttpCodes.NoContent)
            .ProducesProblem()
            .WithDescription("Update product")
            .WithName(nameof(UpdateProduct));
    }

    private static Task<IResult> UpdateProduct(
        Guid productId,
        [FromBody] ProductUpdateRequest request,
        ISender sender,
        CancellationToken cancellationToken = default) =>
            sender
                .Send(request.ToCommand(productId), cancellationToken)
                .Match(
                    () => TypedResults.NoContent(),
                    errors => errors.ToProblemResult(),
                    cancellationToken);
}
