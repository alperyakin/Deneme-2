using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Commands.Increase;

internal sealed class IncreaseStockCommandHandler(
    IStockCommandRepository repository) : ICommandHandler<IncreaseStockCommand>
{
    public Task<Result> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        => repository.IncreaseStockAsync(request.ProductId, request.Amount, cancellationToken);
}
