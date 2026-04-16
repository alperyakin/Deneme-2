using CSharpEssentials;

namespace Deneme2.Services.StockService.Domain.Stocks.Fields;

public readonly record struct StockId
{
    private StockId(Guid value) => Value = value;
    public Guid Value { get; }
    public static StockId New() => new(Guider.NewGuid());
    public static StockId From(Guid value) => new(value);
    public static readonly StockId Empty = new(Guid.Empty);
}
