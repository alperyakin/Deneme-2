using Deneme2.BuildingBlocks.Persistence.Extensions;
using Deneme2.BuildingBlocks.Persistence.HttpHandlers;
using Deneme2.Services.Info;
using Deneme2.Services.ProductService.Persistence.Auth.HttpServices;
using Deneme2.Services.ProductService.Persistence.Categories.HttpServices;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Deneme2.Services.ProductService.Persistence.ServiceRegistrations;
internal static class HttpServicesRegistrations
{
    public static IServiceCollection RegisterHttpServices(this IServiceCollection services)
    {
        services.AddTransient<AuthHeaderHandler>();
        services
            .AddRefitClient<IHttpCategoryService>()
            .ConfigureHttpClientWithServiceName(ServiceKeys.CategoryService)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        services
            .AddRefitClient<IKeycloakApi>()
            .ConfigureHttpClientWithServiceName(ServiceKeys.Keycloak);

        services
            .AddHttpClient<Deneme2.Services.ProductService.Application.Services.IStockServiceClient, Deneme2.Services.ProductService.Persistence.Services.StockServiceClient>()
            .ConfigureHttpClientWithServiceName(ServiceKeys.StockService);

        return services;
    }
}
