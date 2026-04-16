using CSharpEssentials;

namespace Deneme2.Services.StockService.Domain.Stocks.Repositories;

public interface IStockCommandRepository
{
    Task CreateStockAsync(Stock stock, CancellationToken cancellationToken = default);
    Task<Result> IncreaseStockAsync(Guid productId, int amount, CancellationToken cancellationToken = default);
    Task<Result> DecreaseStockAsync(Guid productId, int amount, CancellationToken cancellationToken = default);
}
