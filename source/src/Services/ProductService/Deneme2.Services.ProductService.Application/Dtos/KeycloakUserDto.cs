using System.Text.Json.Serialization;

namespace Deneme2.Services.ProductService.Application.Dtos;

public record KeycloakUserDto(
    string username,
    string email,
    string firstName,
    string lastName,
    bool enabled,
    KeycloakCredentialDto[] credentials
);

public record KeycloakCredentialDto(
    string type,
    string value,
    bool temporary
);

public record KeycloakTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("refresh_token")] string RefreshToken,
    [property: JsonPropertyName("token_type")] string TokenType
);
