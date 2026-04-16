using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Commands.Increase;

public sealed record IncreaseStockCommand(Guid ProductId, int Amount) : ICommand;
