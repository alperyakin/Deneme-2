using CSharpEssentials.EntityFrameworkCore.Interceptors;
using Deneme2.BuildingBlocks.BackgroundJobs.Quartz;
using Deneme2.BuildingBlocks.Caching.Redis;
using Deneme2.BuildingBlocks.Database.PostgreSQL;
using Deneme2.BuildingBlocks.MessageBrokers.MassTransit;
using Deneme2.Services.Info;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Deneme2.Services.ProductService.Persistence.ServiceRegistrations;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        return services
            .ConfigureCacheServices(environment, configuration)
            .AddQuartzWithHostedService()
            .AddDatabaseServices(configuration)
            .AddEventBus(
                configure => configure.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configuration.GetConnectionString(ServiceKeys.RabbitMQ));
                        cfg.ConfigureEndpoints(context);
                    }),
                assemblies: PersistenceAssemblyReference.Assembly)
            .RegisterRepositories()
            .RegisterHttpServices()
            .RegisterServices();
    }
    private static IServiceCollection ConfigureCacheServices(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        return services
            .AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(sp =>
            {
                string connectionString = configuration.GetConnectionString(ServiceKeys.Redis);
                var configurationOptions = ConfigurationOptions.Parse(connectionString!, true);
                return ConnectionMultiplexer.Connect(configurationOptions);
            })
            .AddCacheServices(
                environment.ApplicationName,
                redisConfigurations => redisConfigurations.Configuration = configuration.GetConnectionString(ServiceKeys.Redis),
                memoryConfiguration =>
                {
                });
    }
    private static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string migrationsAssembly = typeof(PersistenceAssemblyReference).Namespace;
        return services
            .AddSingleton<SlowQueryInterceptor>()
            .AddPooledPostgresDbContext<ApplicationWriteDbContext>(
                configuration.GetConnectionString(ServiceKeys.Database.PostgresProductService)!,
                options => options.MigrationsAssembly = migrationsAssembly)
            .AddPooledPostgresDbContext<ApplicationReadDbContext>(
                configuration.GetConnectionString(ServiceKeys.Database.PostgresProductService)!,
                options =>
                {
                    options.MigrationsAssembly = migrationsAssembly;
                    options.EnablePublishDomainEventsInterceptor = false;
                    options.EnableAuditableInterceptor = false;
                    options.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                });
    }
}
