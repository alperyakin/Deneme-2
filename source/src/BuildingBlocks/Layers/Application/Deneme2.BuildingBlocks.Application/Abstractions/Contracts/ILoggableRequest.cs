namespace Deneme2.BuildingBlocks.Application.Abstractions.Contracts;
public interface ILoggableRequest;
public interface IRequestLoggable : ILoggableRequest;
public interface IResponseLoggable : ILoggableRequest;
public interface IRequestResponseLoggable : IRequestLoggable, IResponseLoggable;

