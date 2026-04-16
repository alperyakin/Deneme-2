using CSharpEssentials;
using MediatR;

namespace Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

public interface IQuery : IRequest<Result>;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
