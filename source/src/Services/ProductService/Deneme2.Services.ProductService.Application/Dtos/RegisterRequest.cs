namespace Deneme2.Services.ProductService.Application.Dtos;

public record RegisterRequest(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName
);
