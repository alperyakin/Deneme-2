using CSharpEssentials;
using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Commands.Decrease;

internal sealed class DecreaseStockCommandHandler(
    IStockCommandRepository repository) : ICommandHandler<DecreaseStockCommand>
{
    public Task<Result> Handle(DecreaseStockCommand request, CancellationToken cancellationToken)
        => repository.DecreaseStockAsync(request.ProductId, request.Amount, cancellationToken);
}
