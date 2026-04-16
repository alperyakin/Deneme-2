using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Exist;
public sealed record CategoryExistQuery(Guid CategoryId) : IQuery<bool>;
