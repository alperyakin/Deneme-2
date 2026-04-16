using MediatR;
using Deneme2.BuildingBlocks.Caching.Base;
using Deneme2.BuildingBlocks.MessageBrokers.Base;
using Deneme2.IntegrationEvents.Categories;
using Deneme2.Services.CategoryService.Domain.Categories.Events;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.CategoryService.Application.Categories.v1.DomainEventHandlers;

internal sealed class CategoryDeletedDomainEventHandler(
    ILogger<CategoryDeletedDomainEvent> logger,
    IEventBus eventBus,
    ICacheService cacheService) : INotificationHandler<CategoryDeletedDomainEvent>
{
    public async Task Handle(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("CategoryDeletedDomainEvent handled");
        string CategoryIdCache = $"category:{notification.Id}";
        cacheService.Remove(CategoryIdCache);
        await eventBus.PublishAsync(new CategoryDeletedIntegrationEvent(notification.Id.Value), isTransactional: false, cancellationToken);
    }
}
