using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.StockService.Application.Stocks.v1.Models;
using Deneme2.Services.StockService.Domain.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.ReadModels;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Queries.GetByProductId;

internal sealed class GetStockByProductIdQueryHandler(
    IStockQueryRepository stockQueryRepository) : IQueryHandler<GetStockByProductIdQuery, StockViewModel>
{
    public async Task<Result<StockViewModel>> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
    {
        Maybe<StockReadModel> stock = await stockQueryRepository.GetStockByProductIdAsync(request.ProductId, cancellationToken);

        return stock.Match<Result<StockViewModel>>(
            value => StockViewModel.Create(value),
            () => StockErrors.StockNotFoundError(request.ProductId));
    }
}
