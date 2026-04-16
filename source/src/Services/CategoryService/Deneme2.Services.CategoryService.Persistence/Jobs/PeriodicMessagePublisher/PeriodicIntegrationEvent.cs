namespace Deneme2.IntegrationEvents.Jobs;

public sealed record PeriodicIntegrationEvent(string JobInstanceId, DateTime Timestamp);