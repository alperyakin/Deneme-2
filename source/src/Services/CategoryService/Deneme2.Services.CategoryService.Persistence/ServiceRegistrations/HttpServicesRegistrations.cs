using Deneme2.BuildingBlocks.Persistence.HttpHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.CategoryService.Persistence.ServiceRegistrations;
internal static class HttpServicesRegistrations
{
    public static IServiceCollection RegisterHttpServices(this IServiceCollection services)
    {
        services.AddTransient<AuthHeaderHandler>();

        return services;
    }
}
