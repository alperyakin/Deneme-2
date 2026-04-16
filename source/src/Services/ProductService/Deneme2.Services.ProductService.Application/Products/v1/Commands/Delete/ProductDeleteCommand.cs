using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

namespace Deneme2.Services.ProductService.Application.Products.v1.Commands.Delete;
public sealed record ProductDeleteCommand(Guid ProductId) : ICommand;
