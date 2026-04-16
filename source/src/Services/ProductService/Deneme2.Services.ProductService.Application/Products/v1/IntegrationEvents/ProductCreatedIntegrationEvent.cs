namespace Deneme2.IntegrationEvents.Products;
public sealed record ProductCreatedIntegrationEvent(Guid ProductId, string Name, string Description, decimal Price, string Currency);
