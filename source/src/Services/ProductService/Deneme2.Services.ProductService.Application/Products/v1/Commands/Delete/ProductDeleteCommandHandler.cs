using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.Repositories;

using Deneme2.Services.ProductService.Application.Services;
using Deneme2.Services.ProductService.Domain.Products;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Delete;

internal sealed class ProductDeleteCommandHandler(
    IStockServiceClient stockServiceClient,
    IProductCommandRepository repository) : ICommandHandler<ProductDeleteCommand>
{
    public async Task<Result> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        var productId = ProductId.From(request.ProductId);

        // Olay senkron: Stokta mal varsa silemeyiz.
        int stockQuantity = await stockServiceClient.GetStockQuantityAsync(request.ProductId, cancellationToken);
        if (stockQuantity > 0)
        {
            return ProductErrors.CannotDeleteDueToStockError;
        }

        return await repository.DeleteProductAsync(productId, cancellationToken);
    }
}
