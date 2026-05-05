using Deneme2.IntegrationEvents.Products;
using Deneme2.Services.StockService.Domain.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.StockService.Persistence.Products.IntegrationEventHandlers;

public sealed class ProductCreatedIntegrationEventHandler(
    IStockCommandRepository stockCommandRepository,
    ILogger<ProductCreatedIntegrationEventHandler> logger) : IConsumer<ProductCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductCreatedIntegrationEvent> context)
    {
        var message = context.Message;
        logger.LogInformation("Event alındı - Hedef StockService : ProductCreatedIntegrationEvent. ProductId: {ProductId}",
            message.ProductId);

        var stock = Stock.Create(message.ProductId, initialQuantity: 0);
        await stockCommandRepository.CreateStockAsync(stock, context.CancellationToken);

        logger.LogInformation("Stock kaydı oluşturuldu. ProductId: {ProductId}, Quantity: 0", message.ProductId);
    }
}
