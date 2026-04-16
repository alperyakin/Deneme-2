using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.CategoryService.Application.Categories.v1.Queries.List;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;

namespace Deneme2.Services.CategoryService.WebApi.Endpoints.Categories.v1.List;

public sealed class CategoryListEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Categories);
            //.RequireAuthorization(); // Temporarily disabled for testing

        routeGroup.MapGet(string.Empty, GetCategories)
        .Produces<CategoryViewModel[]>()
        .ProducesProblem()
        .WithDescription("Get Categories")
        .WithName(nameof(GetCategories));
    }

    private static Task<IResult> GetCategories(ISender sender, CancellationToken cancellationToken = default) =>
            sender
               .Send(new GetCategoryListQuery(), cancellationToken)
               .Match(
                     Categorys => TypedResults.Ok(Categorys),
                     errors => errors.ToProblemResult(),
                     cancellationToken);
}
