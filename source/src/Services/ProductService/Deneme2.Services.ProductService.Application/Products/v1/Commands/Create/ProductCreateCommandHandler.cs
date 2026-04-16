using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.ProductService.Domain.Categories.Rules.Exist;
using Deneme2.Services.ProductService.Domain.Categories.Services;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.Parameters;
using Deneme2.Services.ProductService.Domain.Products.Repositories;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Create;

internal sealed class ProductCreateCommandHandler(
    IProductCommandRepository repository,
    ICategoryService categoryService) : ICommandHandler<ProductCreateCommand, ProductId>
{
    public Task<Result<ProductId>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        var rule = new CategoryExistRule(categoryService);
        ProductCreateParameters parameters = request.ToParameters();
        return repository.CreateProductAsync(parameters, rule, cancellationToken);
    }
}
