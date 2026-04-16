using Deneme2.IntegrationEvents.Stocks;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Stocks.IntegrationEventHandlers;

public sealed class StockQuantityChangedEventConsumer(
    ApplicationWriteDbContext context,
    ILogger<StockQuantityChangedEventConsumer> logger) : IConsumer<StockQuantityChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<StockQuantityChangedIntegrationEvent> contextWrapper)
    {
        var message = contextWrapper.Message;
        var productId = ProductId.From(message.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == productId, contextWrapper.CancellationToken);

        if (product is null)
        {
            logger.LogWarning("StockQuantityChangedEvent received but product {ProductId} not found.", message.ProductId);
            return;
        }

        if (message.NewQuantity < 5)
        {
            product.MarkAsLowStock();
            logger.LogInformation("Product {ProductId} marked as Low Stock (Quantity: {NewQty}).", message.ProductId, message.NewQuantity);
        }
        else if (message.NewQuantity >= 5)
        {
            product.MarkAsInStock();
            logger.LogInformation("Product {ProductId} restored from Low Stock (Quantity: {NewQty}).", message.ProductId, message.NewQuantity);
        }

        await context.SaveChangesAsync(contextWrapper.CancellationToken);
    }
}
