using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
using Deneme2.Services.ProductService.Domain.Products.Fields;
using Deneme2.Services.ProductService.Domain.Products.Parameters;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Update;

public sealed record ProductUpdateCommand(
    Guid Id,
    string? Name,
    string? Description,
    decimal Price,
    Currency Currency) : ICommand
{
    public ProductUpdateParameters ToParameters() =>
        new(ProductId.From(Id), Name, Description, Price, Currency);
}
