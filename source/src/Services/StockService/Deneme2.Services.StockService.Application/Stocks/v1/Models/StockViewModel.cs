using Deneme2.Services.StockService.Domain.Stocks.ReadModels;

namespace Deneme2.Services.StockService.Application.Stocks.v1.Models;

public readonly record struct StockViewModel
{
    private StockViewModel(StockReadModel stock)
    {
        Id = stock.Id;
        ProductId = stock.ProductId;
        Quantity = stock.Quantity;
        CreatedAt = stock.CreatedAt;
        UpdatedAt = stock.UpdatedAt;
    }

    public readonly Guid Id { get; init; }
    public readonly Guid ProductId { get; init; }
    public readonly int Quantity { get; init; }
    public readonly DateTimeOffset CreatedAt { get; init; }
    public readonly DateTimeOffset UpdatedAt { get; init; }

    public static StockViewModel Create(StockReadModel stock) => new(stock);
}
