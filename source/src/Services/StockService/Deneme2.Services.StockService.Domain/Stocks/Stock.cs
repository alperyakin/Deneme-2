using CSharpEssentials;
using CSharpEssentials.Entity;
using Deneme2.Services.StockService.Domain.Stocks.Events;
using Deneme2.Services.StockService.Domain.Stocks.Fields;

namespace Deneme2.Services.StockService.Domain.Stocks;

public sealed class Stock : EntityBase<StockId>
{
    private Stock() { }

    private Stock(StockId id, Guid productId, int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public static Stock Create(Guid productId, int initialQuantity = 0)
        => new(StockId.New(), productId, initialQuantity);

    public Result Increase(int amount)
    {
        if (amount <= 0)
            return StockErrors.InvalidAmountError;

        int oldQuantity = Quantity;
        Quantity += amount;

        Raise(new StockQuantityChangedDomainEvent(Id, ProductId, oldQuantity, Quantity));

        return Result.Success();
    }

    public Result Decrease(int amount)
    {
        if (amount <= 0)
            return StockErrors.InvalidAmountError;
        if (Quantity < amount)
            return StockErrors.InsufficientStockError(Quantity, amount);

        int oldQuantity = Quantity;
        Quantity -= amount;

        Raise(new StockQuantityChangedDomainEvent(Id, ProductId, oldQuantity, Quantity));

        return Result.Success();
    }
}
