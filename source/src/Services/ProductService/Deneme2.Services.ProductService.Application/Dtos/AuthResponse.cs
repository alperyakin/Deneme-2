namespace Deneme2.Services.ProductService.Application.Dtos;

public record AuthResponse(
    string AccessToken,
    int ExpiresIn,
    string RefreshToken
);
