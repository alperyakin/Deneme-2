using CSharpEssentials.Interfaces;

namespace Deneme2.Services.StockService.Domain.Stocks.ReadModels;

public sealed class StockReadModel
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
