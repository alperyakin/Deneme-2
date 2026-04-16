using System.Net.Http.Json;
using Deneme2.Services.ProductService.Application.Services;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Services;

internal sealed class StockServiceClient(
    HttpClient httpClient,
    ILogger<StockServiceClient> logger) : IStockServiceClient
{
    private sealed record StockResponse(int Quantity);

    public async Task<int> GetStockQuantityAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.GetAsync($"/api/v1/stocks/{productId}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var stock = await response.Content.ReadFromJsonAsync<StockResponse>(cancellationToken: cancellationToken);
                return stock?.Quantity ?? 0;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return 0; // Eğer stok henüz açılmadıysa 0 sayalım.
            }

            logger.LogWarning("Failed to fetch stock for ProductId: {ProductId}. Status: {StatusCode}", productId, response.StatusCode);
            return 0; // Hata durumunda 0 dönüyoruz (veya isterseniz exception da atılabilir).
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while calling StockService for ProductId: {ProductId}", productId);
            return 0;
        }
    }
}
