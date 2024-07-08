using DotNetCore.CAP;
using MDSF.BuildingBlocks.Domain.Event;
using Microsoft.Extensions.Logging;
using static MDSF.BuildingBlocks.CAP.Extensions;

namespace MDSF.BuildingBlocks.CAP;

public sealed class BusPublisher : IBusPublisher
{
    private readonly ILogger<BusPublisher> _logger;
    private readonly ICapPublisher _capPublisher;

    public BusPublisher(ILogger<BusPublisher> logger,
        ICapPublisher capPublisher)
    {
        _logger = logger;
        _capPublisher = capPublisher;
        
    }

    public async Task SendAsync(IDomainEvent domainEvent,
        CancellationToken cancellationToken = default) => await SendAsync(new[] { domainEvent }, cancellationToken);

    public async Task SendAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        if (domainEvents is null) return;
     
        _logger.LogTrace("Processing domain events start...");

        foreach (var integrationEvent in domainEvents)
        {
            
            await _capPublisher.PublishAsync(integrationEvent.GetType().Name + CapBusName.Monolit, integrationEvent, cancellationToken: cancellationToken);
            await _capPublisher.PublishAsync(integrationEvent.GetType().Name + CapBusName.MicroService, integrationEvent, cancellationToken: cancellationToken);

            _logger.LogTrace("Publish a message with ID: {Id}", integrationEvent?.EventId);
        }

        _logger.LogTrace("Processing integration events done...");
    }
}
