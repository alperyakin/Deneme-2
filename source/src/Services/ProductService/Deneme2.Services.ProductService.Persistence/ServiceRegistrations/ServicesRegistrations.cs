using Deneme2.Services.ProductService.Domain.Categories.Services;
using Deneme2.Services.ProductService.Persistence.Auth.Services;
using Deneme2.Services.ProductService.Persistence.Categories.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.ProductService.Persistence.ServiceRegistrations;

internal static class ServicesRegistrations
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddTransient<KeycloakIdentityService>();
        return services;
    }
}
