using MediatR;
using Deneme2.BuildingBlocks.Caching.Base;
using Deneme2.BuildingBlocks.MessageBrokers.Base;
using Deneme2.IntegrationEvents.Products;
using Deneme2.Services.ProductService.Domain.Products.Events;
using Deneme2.Services.ProductService.Domain.Products.Repositories;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Application.Products.v1.Events;
internal sealed class ProductCreatedDomainEventHandler(
    ILogger<ProductCreatedDomainEventHandler> logger,
    ICacheService cacheService,
    IProductQueryRepository productQueryRepository,
    IEventBus eventBus) : INotificationHandler<ProductCreatedDomainEvent>
{
    private const string Tag = "products";

    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("ProductCreatedDomainEvent handled for ID: {Id}", notification.Id.Value);
        await cacheService.InvalidateTagAsync(Tag);
        
        var productResult = await productQueryRepository.GetProductByIdAsync(notification.Id, cancellationToken);
        if (productResult.HasValue)
        {
            var p = productResult.Value;
            var integrationEvent = new ProductCreatedIntegrationEvent(p.Id, p.Name, p.Description, p.Price, p.Currency.ToString());
            await eventBus.PublishAsync(integrationEvent, isTransactional: false, cancellationToken);
        }
    }
}
