
using Deneme2.IntegrationEvents.Jobs;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Deneme2.Services.ProductService.Persistence.Jobs.IntegrationEventHandlers;

public sealed class PeriodicIntegrationEventHandler(
    ILogger<PeriodicIntegrationEventHandler> logger
) : IConsumer<PeriodicIntegrationEvent>
{
    public Task Consume(ConsumeContext<PeriodicIntegrationEvent> context)
    {
        logger.LogInformation("Periodic integration event received {InstanceId}, {Timestamp}", context.Message.JobInstanceId, context.Message.Timestamp);
        return Task.CompletedTask;
    }
}
