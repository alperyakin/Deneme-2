using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.StockService.Application.Stocks.v1.Models;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Queries.GetByProductId;

public sealed record GetStockByProductIdQuery(Guid ProductId) : IQuery<StockViewModel>
{
}
