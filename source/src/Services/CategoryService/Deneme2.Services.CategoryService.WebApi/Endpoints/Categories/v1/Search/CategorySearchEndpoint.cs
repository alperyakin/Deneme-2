using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Search;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;

namespace Deneme2.Services.CategoryService.WebApi.Endpoints.Categories.v1.Search;

public sealed class CategorySearchEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Categories);
            //.RequireAuthorization(); // Temporarily disabled for testing

        routeGroup.MapGet("search", SearchCategories)
            .Produces<CategoryViewModel[]>()
            .ProducesProblem()
            .WithDescription("Search categories by name")
            .WithName(nameof(SearchCategories));
    }

    private static Task<IResult> SearchCategories(
        string? name,
        ISender sender,
        CancellationToken cancellationToken = default) =>
            sender
               .Send(new SearchCategoryByNameQuery(name), cancellationToken)
               .Match(
                     categories => TypedResults.Ok(categories),
                     errors => errors.ToProblemResult(),
                     cancellationToken);
}
