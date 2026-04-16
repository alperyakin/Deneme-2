using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.ProductService.Application.Products.v1.Models;
using Deneme2.Services.ProductService.Domain.Products;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.ReadModels;
using Deneme2.Services.ProductService.Domain.Products.Repositories;

namespace Deneme2.Services.ProductService.Application.Products.v1.Queries.Get;

internal sealed class GetProductQueryHandler(
    IProductQueryRepository productQueryRepository) : ICachedQueryHandler<GetProductQuery, ProductViewModel>
{
    public async Task<Result<ProductViewModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productId = ProductId.From(request.ProductId);
        Maybe<ProductReadModel> product = await productQueryRepository.GetProductByIdAsync(productId, cancellationToken);

        return product.Match<Result<ProductViewModel>>(
            value => ProductViewModel.Create(value),
            () => ProductErrors.ProductDoesNotExistError(productId));
    }
}
