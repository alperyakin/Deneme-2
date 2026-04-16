using MediatR;
using Deneme2.BuildingBlocks.Caching.Base;
using Deneme2.Services.ProductService.Domain.Products.Events;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Application.Products.v1.Events;

internal sealed class ProductDeletedDomainEventHandler(
    ILogger<ProductCreatedDomainEventHandler> logger,
    ICacheService cacheService) : INotificationHandler<ProductDeletedDomainEvent>
{
    private const string Tag = "products:list";
    public Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("ProductDeletedDomainEvent handled");
        string productIdCache = $"product:{notification.Id}";
        cacheService.Remove(productIdCache);
        return cacheService.InvalidateTagAsync(Tag);
    }
}
