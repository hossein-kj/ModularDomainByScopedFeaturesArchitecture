using MDSF.BuildingBlocks.Domain.Event;
using MDSF.BuildingBlocks.Domain.Model;

namespace MDSF.BuildingBlocks.EventStoreDB.Events
{
    public interface IAggregateEventSourcing : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
        long Version { get; }
    }

    public interface IAggregateEventSourcing<out T> : IAggregateEventSourcing
    {
        T Id { get; }
    }
}


