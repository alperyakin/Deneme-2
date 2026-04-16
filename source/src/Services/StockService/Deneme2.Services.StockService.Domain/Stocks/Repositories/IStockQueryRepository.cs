using CSharpEssentials;
using Deneme2.Services.StockService.Domain.Stocks.ReadModels;

namespace Deneme2.Services.StockService.Domain.Stocks.Repositories;

public interface IStockQueryRepository
{
    Task<Maybe<StockReadModel>> GetStockByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
}
