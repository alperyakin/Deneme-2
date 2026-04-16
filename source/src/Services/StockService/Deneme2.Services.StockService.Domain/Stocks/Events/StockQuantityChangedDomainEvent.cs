using CSharpEssentials.Interfaces;
using Deneme2.Services.StockService.Domain.Stocks.Fields;

namespace Deneme2.Services.StockService.Domain.Stocks.Events;

public sealed record StockQuantityChangedDomainEvent(StockId StockId, Guid ProductId, int OldQuantity, int NewQuantity) : IDomainEvent;
