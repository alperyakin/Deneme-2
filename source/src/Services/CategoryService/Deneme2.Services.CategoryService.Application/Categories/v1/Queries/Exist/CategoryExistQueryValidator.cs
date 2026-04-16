using FluentValidation;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Exist;

internal sealed class CategoryExistQueryValidator : AbstractValidator<CategoryExistQuery>
{
    public CategoryExistQueryValidator() => RuleFor(x => x.CategoryId).NotEmpty();
}
