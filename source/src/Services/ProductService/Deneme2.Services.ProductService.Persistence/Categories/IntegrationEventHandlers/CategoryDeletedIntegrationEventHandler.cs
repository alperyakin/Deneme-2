using CSharpEssentials;
using Deneme2.IntegrationEvents.Categories;
using Deneme2.Services.ProductService.Domain.Categories.Repositories;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Categories.IntegrationEventHandlers;

public sealed class CategoryDeletedIntegrationEventHandler(
    ICategoryCommandRepository categoryCommandRepository,
    ILogger<CategoryDeletedIntegrationEventHandler> logger) : IConsumer<CategoryDeletedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<CategoryDeletedIntegrationEvent> context)
    {
        Guid categoryId = context.Message.CategoryId;
        Result result = await categoryCommandRepository.DeleteProductsByCategoryIdAsync(CategoryId.From(categoryId), context.CancellationToken);
        logger.LogInformation("Category with id {CategoryId} has been deleted. Result: {Result}", categoryId, result);
    }
}
