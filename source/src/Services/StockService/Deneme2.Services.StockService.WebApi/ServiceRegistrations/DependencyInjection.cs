using System.Reflection;
using Carter;
using CSharpEssentials;
using CSharpEssentials.AspNetCore;
using CSharpEssentials.RequestResponseLogging;
using Deneme2.BuildingBlocks.Application.Shared.Constants;
using Deneme2.BuildingBlocks.Presentation.Authentication;
using Deneme2.BuildingBlocks.Presentation.Cors;
using Deneme2.BuildingBlocks.Presentation.HealthChecks;
using Deneme2.BuildingBlocks.Presentation.SessionContexts;
using Deneme2.Services.Info;
using Deneme2.Services.StockService.Application.ServiceRegistrations;
using Deneme2.Services.StockService.Persistence.ServiceRegistrations;
using Microsoft.Net.Http.Headers;

namespace Deneme2.Services.StockService.WebApi.ServiceRegistrations;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServiceRegistrations(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration)
    {
        services.AddCarter();
        services.AddHealthChecks();

        return services
            .AddAllAcceptCors()
            .AddHttpContextAccessor()
            .AddApplicationServices()
            .AddPersistenceServices(hostEnvironment, configuration)
            .AddSessionContext()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .ConfigureModelValidatorResponse()
            .ConfigureSystemTextJson()
            .AddEnhancedProblemDetails()
            .AddAndConfigureApiVersioning()
            .AddSwagger<DefaultConfigureSwaggerOptions>(SecuritySchemes.JwtBearerTokenSecurity, Assembly.GetExecutingAssembly())
            .AddKeycloakJwtBearer(keycloakServiceId: ServiceKeys.Keycloak, realm: "product")
            .ConfigureTelemetries(hostEnvironment);
    }

    internal static WebApplication UseServices(
        this WebApplication app)
    {
        app.UseVersionableSwagger();
        app.AddRequestResponseLogging(opt =>
        {
            opt.IgnorePaths("/health");
            var loggingOptions = LoggingOptions.CreateAllFields();
            loggingOptions.HeaderKeys.Add(HeaderNames.AcceptLanguage);
            loggingOptions.HeaderKeys.Add(CustomHeaderNames.TenantId);
            opt.UseLogger(app.Services.GetRequiredService<ILoggerFactory>(), loggingOptions);
        });
        app.UseExceptionHandler();
        app.UseStatusCodePages();
        app.UseCors("all");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapCarter();
        app.UseDefaultHealthChecks();

        return app;
    }
}
