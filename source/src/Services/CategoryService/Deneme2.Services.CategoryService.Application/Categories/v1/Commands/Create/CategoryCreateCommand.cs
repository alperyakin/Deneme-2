using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Domain.Categories.Fields;
using Deneme2.Services.CategoryService.Domain.Categories.Parameters;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Commands.Create;

public sealed record CategoryCreateCommand(
    string? Name) : ICommand<CategoryId>
{
    public CategoryCreateParameters ToParameters() =>
        new(Name);
}
