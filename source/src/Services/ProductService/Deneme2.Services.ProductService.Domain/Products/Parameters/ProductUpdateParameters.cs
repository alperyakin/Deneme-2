using Deneme2.Services.ProductService.Domain.Products.Fields;

namespace Deneme2.Services.ProductService.Domain.Products.Parameters;

public sealed record ProductUpdateParameters(
    ProductId Id,
    string? Name,
    string? Description,
    decimal Price,
    Currency Currency);
