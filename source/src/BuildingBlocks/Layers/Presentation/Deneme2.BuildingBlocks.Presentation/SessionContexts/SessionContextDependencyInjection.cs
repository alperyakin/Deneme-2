using Deneme2.BuildingBlocks.Application.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.BuildingBlocks.Presentation.SessionContexts;
public static class SessionContextDependencyInjection
{
    public static IServiceCollection AddSessionContext(this IServiceCollection services) =>
        services.AddSessionContext<ISessionContext, DefaultSessionContext>();

    public static IServiceCollection AddSessionContext
        <TISessionContext, TSessionContext>(this IServiceCollection services)
        where TISessionContext : ISessionContext
        where TSessionContext : class, TISessionContext
    {
        services.AddScoped<IUserContext, TSessionContext>();
        services.AddScoped<ITenantContext, TSessionContext>();
        services.AddScoped<ISessionContext, TSessionContext>();
        return services;
    }
}
