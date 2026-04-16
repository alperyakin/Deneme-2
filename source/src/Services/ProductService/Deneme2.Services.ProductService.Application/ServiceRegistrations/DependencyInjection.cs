using FluentValidation;
using Deneme2.BuildingBlocks.Application.DependencyInjections;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.ProductService.Application.ServiceRegistrations;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        return services
            .AddDateTimeProvider()
            .AddMediatR(configure =>
            {
                configure.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly);
                configure.AddDefaultBehaviors();
            })
            .AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true);
    }
}
