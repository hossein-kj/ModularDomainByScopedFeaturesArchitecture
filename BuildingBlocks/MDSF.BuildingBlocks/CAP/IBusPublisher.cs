using MDSF.BuildingBlocks.Domain.Event;

namespace MDSF.BuildingBlocks.CAP;

public interface IBusPublisher
{
    public Task SendAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    public Task SendAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
