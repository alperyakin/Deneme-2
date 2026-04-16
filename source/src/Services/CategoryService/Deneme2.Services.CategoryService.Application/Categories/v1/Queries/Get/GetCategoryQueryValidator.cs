using FluentValidation;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.Queries.Get;

internal sealed class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator() => RuleFor(x => x.CategoryId).NotEmpty();
}
