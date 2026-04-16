using Deneme2.BuildingBlocks.MessageBrokers.Base;
using Deneme2.IntegrationEvents.Stocks;
using Deneme2.Services.StockService.Domain.Stocks.Events;

using MediatR;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Events;

internal sealed class StockQuantityChangedDomainEventHandler(
    IEventBus eventBus,
    ILogger<StockQuantityChangedDomainEventHandler> logger) : INotificationHandler<StockQuantityChangedDomainEvent>
{
    public async Task Handle(StockQuantityChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Check if crossed the threshold of 5
        bool wasLow = notification.OldQuantity < 5;
        bool isLow = notification.NewQuantity < 5;

        // Only publish if the state changed from normal -> low, or low -> normal
        if (wasLow != isLow)
        {
            logger.LogInformation("Stock threshold crossed. ProductId: {ProductId}, Old: {Old}, New: {New}. Publishing Integration Event.",
                notification.ProductId, notification.OldQuantity, notification.NewQuantity);

            var integrationEvent = new StockQuantityChangedIntegrationEvent(
                notification.ProductId,
                notification.OldQuantity,
                notification.NewQuantity);

            await eventBus.PublishAsync(integrationEvent, cancellationToken: cancellationToken);
        }
    }
}
