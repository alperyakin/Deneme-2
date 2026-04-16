using Deneme2.Services.StockService.Domain.Stocks.Repositories;
using Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Repositories.Stocks;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.StockService.Persistence.ServiceRegistrations;

internal static class RepositoryRegistrations
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IStockCommandRepository, EfStockCommandRepository>()
            .AddScoped<IStockQueryRepository, EfStockQueryRepository>();
}
