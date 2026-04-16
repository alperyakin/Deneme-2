using Deneme2.Services.ProductService.Application.Dtos;
using Deneme2.Services.ProductService.Persistence.Auth.HttpServices;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Auth.Services;

public class KeycloakIdentityService
{
    private readonly IKeycloakApi _keycloakApi;
    private readonly ILogger<KeycloakIdentityService> _logger;
    private const string Realm = "product";

    public KeycloakIdentityService(IKeycloakApi keycloakApi, ILogger<KeycloakIdentityService> logger)
    {
        _keycloakApi = keycloakApi;
        _logger = logger;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting login process for username: {Username}", request.Username);

        try
        {
            var body = new Dictionary<string, string>
            {
                { "username", request.Username },
                { "password", request.Password },
                { "client_id", "product-api" },
                { "grant_type", "password" }
            };

            // Eğer client confidential ise, client_secret ekle:
            // body.Add("client_secret", "your-client-secret-here");

            _logger.LogInformation("Requesting token from Keycloak realm: {Realm} with client_id: {ClientId}",
                Realm, "product-api");

            var tokenResponse = await _keycloakApi.GetTokenAsync(Realm, body, cancellationToken);

            _logger.LogInformation("Login successful for username: {Username}", request.Username);

            return new AuthResponse(
                tokenResponse.AccessToken,
                tokenResponse.ExpiresIn,
                tokenResponse.RefreshToken
            );
        }
        catch (Refit.ApiException apiEx)
        {
            _logger.LogError(apiEx,
                "Login failed for username: {Username}. Status: {StatusCode}, Reason: {ReasonPhrase}, Content: {Content}",
                request.Username, apiEx.StatusCode, apiEx.ReasonPhrase, apiEx.Content);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for username: {Username}. Error: {ErrorMessage}",
                request.Username, ex.Message);
            return null;
        }
    }

    public async Task<bool> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting user registration process for username: {Username}, email: {Email}",
            request.Username, request.Email);

        try
        {
            _logger.LogInformation("Step 1: Requesting admin token from Keycloak master realm");

            Dictionary<string, string> adminBody = new Dictionary<string, string>
            {
                { "username", "admin" },
                { "password", "SEzsb7)}GT}0txF)*Us--r" },
                { "client_id", "admin-cli" },
                { "grant_type", "password" }
            };

            var adminToken = await _keycloakApi.GetAdminTokenAsync(adminBody, cancellationToken);

            _logger.LogInformation("Step 1: Successfully obtained admin token");

            _logger.LogInformation("Step 2: Preparing user data for Keycloak");

            var keycloakUser = new KeycloakUserDto(
                username: request.Username,
                email: request.Email,
                firstName: request.FirstName,
                lastName: request.LastName,
                enabled: true,
                credentials: new[]
                {
                    new KeycloakCredentialDto(
                        type: "password",
                        value: request.Password,
                        temporary: false
                    )
                }
            );

            _logger.LogInformation("Step 2: User data prepared successfully");
            _logger.LogInformation("Step 3: Creating user in Keycloak realm: {Realm}", Realm);

            await _keycloakApi.CreateUserAsync(
                Realm,
                keycloakUser,
                $"Bearer {adminToken.AccessToken}",
                cancellationToken
            );

            _logger.LogInformation("Step 3: User created successfully in Keycloak");
            _logger.LogInformation("Registration completed successfully for username: {Username}", request.Username);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed for username: {Username}. Error: {ErrorMessage}",
                request.Username, ex.Message);
            return false;
        }
    }
}
