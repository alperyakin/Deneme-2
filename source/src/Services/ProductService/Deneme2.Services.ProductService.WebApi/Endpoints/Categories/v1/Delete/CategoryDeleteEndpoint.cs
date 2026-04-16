using Carter;
using CSharpEssentials;
using MediatR;
using Deneme2.BuildingBlocks.Presentation.Endpoints;
using Deneme2.Services.ProductService.Application.Categories.v1.Commands.Delete;

namespace Deneme2.Services.ProductService.WebApi.Endpoints.Categories.v1.Delete;

public sealed class CategoryDeleteEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app
            .CreateVersionedGroup(Tags.Categories)
            .RequireAuthorization();

        routeGroup.MapDelete("{categoryId:guid}", DeleteCategory)
            .Produces(HttpCodes.NoContent)
            .ProducesProblem()
            .WithDescription("Delete category by id")
            .WithName(nameof(DeleteCategory));
    }

    private static Task<IResult> DeleteCategory(Guid categoryId, ISender sender, CancellationToken cancellationToken = default) =>
            sender
               .Send(new CategoryDeleteCommand(categoryId), cancellationToken)
               .Match(
                     TypedResults.NoContent,
                     errors => errors.ToProblemResult(),
                     cancellationToken);
}
