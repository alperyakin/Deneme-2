using Carter;
using Deneme2.Services.ProductService.Application.Dtos;
using Deneme2.Services.ProductService.Persistence.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Deneme2.Services.ProductService.WebApi.Endpoints.Auth;

public sealed class AuthEndpoints : CarterModule
{
    public AuthEndpoints() : base("/auth")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", Login)
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("User login")
            .WithName(nameof(Login))
            .AllowAnonymous();

        app.MapPost("/register", Register)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("User registration")
            .WithName(nameof(Register))
            .AllowAnonymous();
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] KeycloakIdentityService identityService,
        CancellationToken cancellationToken = default)
    {
        var result = await identityService.LoginAsync(request, cancellationToken);

        return result is not null
            ? Results.Ok(result)
            : Results.BadRequest(new { message = "Invalid username or password" });
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] KeycloakIdentityService identityService,
        CancellationToken cancellationToken = default)
    {
        var success = await identityService.RegisterAsync(request, cancellationToken);

        return success
            ? Results.Ok(new { message = "User registered successfully" })
            : Results.BadRequest(new { message = "Failed to register user" });
    }
}
