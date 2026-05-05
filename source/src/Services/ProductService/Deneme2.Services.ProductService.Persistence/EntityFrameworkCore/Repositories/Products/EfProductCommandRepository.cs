using CSharpEssentials;
using Deneme2.Services.ProductService.Domain.Products;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.Parameters;
using Deneme2.Services.ProductService.Domain.Products.Repositories;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Repositories.Products;

internal sealed class EfProductCommandRepository(
    ApplicationWriteDbContext context) : IProductCommandRepository
{
    public async Task<Result<ProductId>> CreateProductAsync(ProductCreateParameters parameters, CancellationToken cancellationToken = default)
    {
        Result<Product> productResult = Product.Create(parameters);
        if (productResult.IsFailure)
            return productResult.Errors;
        Product product = productResult.Value;

        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task<Result<ProductId>> CreateProductAsync(ProductCreateParameters parameters, IRuleBase<ProductCreateParameters> rule, CancellationToken cancellationToken = default)
    {
        Result<Product> productResult = Product.Create(parameters, rule, cancellationToken);
        if (productResult.IsFailure)
            return productResult.Errors;
        Product product = productResult.Value;

        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }

    public async Task<Result> UpdateProductAsync(ProductUpdateParameters parameters, CancellationToken cancellationToken = default)
    {
        Product? found = await context.Products
            .Where(product => product.Id == parameters.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return ProductErrors.ProductDoesNotExistError(parameters.Id);

        Result updateResult = found.Update(parameters);
        if (updateResult.IsFailure)
            return updateResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateProductAsync(ProductUpdateParameters parameters, IRuleBase<ProductUpdateParameters> rule, CancellationToken cancellationToken = default)
    {
        Product? found = await context.Products
            .Where(product => product.Id == parameters.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return ProductErrors.ProductDoesNotExistError(parameters.Id);

        Result ruleResult = RuleEngine.Evaluate(rule, parameters, cancellationToken);
        if (ruleResult.IsFailure)
            return ruleResult.Errors;

        Result updateResult = found.Update(parameters);
        if (updateResult.IsFailure)
            return updateResult.Errors;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteProductAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        Product? found = await context.Products
            .Where(product => product.Id == productId)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return ProductErrors.ProductDoesNotExistError(productId);

        found.Delete();
        context.Products.Remove(found);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> MarkAsLowStockAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var id = ProductId.From(productId);
        Product? found = await context.Products
            .Where(product => product.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return ProductErrors.ProductDoesNotExistError(id);

        found.MarkAsLowStock();
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> MarkAsInStockAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var id = ProductId.From(productId);
        Product? found = await context.Products
            .Where(product => product.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (found is null)
            return ProductErrors.ProductDoesNotExistError(id);

        found.MarkAsInStock();
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
