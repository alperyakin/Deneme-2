using CSharpEssentials;
using MediatR;

namespace Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
