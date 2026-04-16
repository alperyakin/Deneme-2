using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.CategoryService.Application.Products.v1.Models;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Get;
public sealed record GetCategoryQuery(Guid CategoryId) : ICachedQuery<CategoryViewModel>
{
    public bool BypassCache => false;

    public bool CacheFailures => true;

    public string CacheKey => $"category:{CategoryId}";

    public TimeSpan Expiration => TimeSpan.FromMinutes(10);

    public string[] Tags => [];
}
