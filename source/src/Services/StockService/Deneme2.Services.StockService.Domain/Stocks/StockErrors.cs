using CSharpEssentials;

namespace Deneme2.Services.StockService.Domain.Stocks;

public static class StockErrors
{
    public static readonly Error InvalidAmountError =
        Error.Validation(code: "Stock.InvalidAmount", description: "Amount must be greater than zero.");

    public static Error InsufficientStockError(int current, int requested) =>
        Error.Validation(code: "Stock.InsufficientStock", description: $"Insufficient stock. Current: {current}, Requested: {requested}.");

    public static Error StockNotFoundError(Guid productId) =>
        Error.NotFound(code: "Stock.NotFound", description: $"Stock record for ProductId '{productId}' was not found.");
}
