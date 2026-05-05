using CSharpEssentials;
using Deneme2.IntegrationEvents.Stocks;
using Deneme2.Services.ProductService.Domain.Products.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Stocks.IntegrationEventHandlers;

public sealed class StockQuantityChangedEventConsumer(
    IProductCommandRepository repository,
    ILogger<StockQuantityChangedEventConsumer> logger) : IConsumer<StockQuantityChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<StockQuantityChangedIntegrationEvent> context)
    {
        var message = context.Message;

        if (message.NewQuantity < 5)
        {
            Result result = await repository.MarkAsLowStockAsync(message.ProductId, context.CancellationToken);
            if (result.IsSuccess)
                logger.LogInformation("Product {ProductId} marked as Low Stock (Quantity: {NewQty}).", message.ProductId, message.NewQuantity);
            else
                logger.LogWarning("Failed to mark product {ProductId} as Low Stock: {Errors}", message.ProductId, result.Errors);
        }
        else
        {
            Result result = await repository.MarkAsInStockAsync(message.ProductId, context.CancellationToken);
            if (result.IsSuccess)
                logger.LogInformation("Product {ProductId} restored from Low Stock (Quantity: {NewQty}).", message.ProductId, message.NewQuantity);
            else
                logger.LogWarning("Failed to mark product {ProductId} as In Stock: {Errors}", message.ProductId, result.Errors);
        }
    }
}
