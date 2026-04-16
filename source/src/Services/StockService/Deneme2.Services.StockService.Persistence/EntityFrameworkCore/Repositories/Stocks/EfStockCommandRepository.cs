using CSharpEssentials;
using Deneme2.Services.StockService.Domain.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;
using Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Repositories.Stocks;

internal sealed class EfStockCommandRepository(
    ApplicationWriteDbContext context) : IStockCommandRepository
{
    public async Task CreateStockAsync(Stock stock, CancellationToken cancellationToken = default)
    {
        await context.Stocks.AddAsync(stock, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> IncreaseStockAsync(Guid productId, int amount, CancellationToken cancellationToken = default)
    {
        Stock? stock = await context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

        if (stock is null)
            return StockErrors.StockNotFoundError(productId);

        Result result = stock.Increase(amount);
        if (result.IsFailure)
            return result;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> DecreaseStockAsync(Guid productId, int amount, CancellationToken cancellationToken = default)
    {
        Stock? stock = await context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

        if (stock is null)
            return StockErrors.StockNotFoundError(productId);

        Result result = stock.Decrease(amount);
        if (result.IsFailure)
            return result;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
