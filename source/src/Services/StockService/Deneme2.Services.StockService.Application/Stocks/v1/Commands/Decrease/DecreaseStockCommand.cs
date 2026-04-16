using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Commands.Decrease;

public sealed record DecreaseStockCommand(Guid ProductId, int Amount) : ICommand;
