using CSharpEssentials.Interfaces;

namespace Deneme2.Services.StockService.Domain.Stocks.ReadModels;

public sealed class StockReadModel : ISoftDeletableBase
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
