using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.ProductService.Domain.Products.Parameters;
using Deneme2.Services.ProductService.Domain.Products.Repositories;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Update;

internal sealed class ProductUpdateCommandHandler(
    IProductCommandRepository repository) : ICommandHandler<ProductUpdateCommand>
{
    public Task<Result> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        ProductUpdateParameters parameters = request.ToParameters();
        return repository.UpdateProductAsync(parameters, cancellationToken);
    }
}
