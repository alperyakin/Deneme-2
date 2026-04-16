using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Search;
public sealed record SearchCategoryByNameQuery(string? Name) :
    ICachedQuery<CategoryViewModel[]>
{
    public bool BypassCache => false;

    public bool CacheFailures => true;

    public string CacheKey => $"categories:search:{Name ?? "all"}";

    public TimeSpan Expiration => TimeSpan.FromMinutes(5);

    public string[] Tags => [];
}
