using Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

namespace Deneme2.Services.ProductService.Application.Categories.v1.Commands.Delete;
public sealed record CategoryDeleteCommand(Guid CategoryId) : ICommand;
