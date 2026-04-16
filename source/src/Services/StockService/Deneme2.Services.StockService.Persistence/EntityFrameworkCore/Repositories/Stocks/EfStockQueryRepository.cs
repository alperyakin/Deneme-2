using CSharpEssentials;
using Deneme2.Services.StockService.Domain.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.ReadModels;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;
using Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Repositories.Stocks;

internal sealed class EfStockQueryRepository(
    ApplicationReadDbContext context) : IStockQueryRepository
{
    public async Task<Maybe<StockReadModel>> GetStockByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await context.Stocks
            .Where(s => s.ProductId == productId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
