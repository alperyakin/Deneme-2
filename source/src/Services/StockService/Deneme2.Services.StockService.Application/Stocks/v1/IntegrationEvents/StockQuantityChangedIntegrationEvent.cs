namespace Deneme2.IntegrationEvents.Stocks;

public sealed record StockQuantityChangedIntegrationEvent(Guid ProductId, int OldQuantity, int NewQuantity);
