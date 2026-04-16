namespace Deneme2.BuildingBlocks.Application.Abstractions.Contracts;

public interface ICachedQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : ICachedQuery<TResponse>;
