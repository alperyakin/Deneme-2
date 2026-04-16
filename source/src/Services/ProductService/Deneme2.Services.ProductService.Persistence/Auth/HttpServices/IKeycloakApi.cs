using Deneme2.Services.ProductService.Application.Dtos;
using Refit;

namespace Deneme2.Services.ProductService.Persistence.Auth.HttpServices;

public interface IKeycloakApi
{
    [Post("/realms/{realm}/protocol/openid-connect/token")]
    Task<KeycloakTokenResponse> GetTokenAsync(
        string realm,
        [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> body,
        CancellationToken cancellationToken = default);

    [Post("/realms/master/protocol/openid-connect/token")]
    Task<KeycloakTokenResponse> GetAdminTokenAsync(
        [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> body,
        CancellationToken cancellationToken = default);

    [Post("/admin/realms/{realm}/users")]
    Task CreateUserAsync(
        string realm,
        [Body] KeycloakUserDto user,
        [Header("Authorization")] string authorization,
        CancellationToken cancellationToken = default);
}
