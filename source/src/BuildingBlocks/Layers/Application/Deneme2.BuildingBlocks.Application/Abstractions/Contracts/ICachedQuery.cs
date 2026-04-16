namespace Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICacheable;
