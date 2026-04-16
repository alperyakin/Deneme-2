using Deneme2.BuildingBlocks.Caching.Redis;
using Deneme2.BuildingBlocks.Database.PostgreSQL;
using Deneme2.BuildingBlocks.OpenTelemetry.Base;

namespace Deneme2.Services.ProductService.WebApi.ServiceRegistrations;

internal static class OpenTelemetryDependencyInjection
{
    internal static IServiceCollection ConfigureTelemetries(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment)
    {
        services
           .ConfigureOpenTelemetry(hostEnvironment.ApplicationName)
           .ConfigurePostgresSqlTelemetry()
           .ConfigureCacheServiceTelemetry()
           .AddOtlpExporter();
        return services;
    }
}
