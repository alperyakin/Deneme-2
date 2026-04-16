using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Deneme2.BuildingBlocks.Presentation.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddKeycloakJwtBearer(
        this IServiceCollection services,
        string keycloakServiceId = "keycloak",
        string realm = "product")
    {
        services
           .AddAuthorization()
           .AddAuthentication()
            .AddKeycloakJwtBearer(keycloakServiceId, realm, options =>
            {
                options.RequireHttpsMetadata = false;
#pragma warning disable CA5404 // Do not disable token validation checks
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
#pragma warning restore CA5404
            });

        return services;
    }
}
