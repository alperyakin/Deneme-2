using FluentValidation;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Delete;

internal sealed class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator() => RuleFor(x => x.ProductId).NotEmpty();
}

