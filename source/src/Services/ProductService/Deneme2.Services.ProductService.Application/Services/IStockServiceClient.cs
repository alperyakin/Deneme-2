namespace Deneme2.Services.ProductService.Application.Services;

public interface IStockServiceClient
{
    Task<int> GetStockQuantityAsync(Guid productId, CancellationToken cancellationToken = default);
}
