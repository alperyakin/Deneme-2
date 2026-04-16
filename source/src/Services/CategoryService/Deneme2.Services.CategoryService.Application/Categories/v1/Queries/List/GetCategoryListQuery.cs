using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.List;
public sealed record GetCategoryListQuery() :
    ICachedQuery<CategoryViewModel[]>
{
    public bool BypassCache => false;

    public bool CacheFailures => true;

    public string CacheKey => $"categories";

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);

    public string[] Tags => [];
}
