using Deneme2.Services.CategoryService.Persistence.Jobs.PeriodicMessagePublisher;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.CategoryService.Persistence.ServiceRegistrations;

public static class JobOptionsRegistrations
{

    public static IServiceCollection ConfigureJobOptions(
        this IServiceCollection services
    )
    {
        services.ConfigureOptions<PeriodicMessagePublisherJobSetup>();
        return services;
    }

}
