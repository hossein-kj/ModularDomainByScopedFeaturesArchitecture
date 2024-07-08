using MDSF.BuildingBlocks.Domain.Event;

namespace MDSF.BuildingBlocks.Domain.Model;

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
    long Version { get; set; }
}

public interface IAggregate<out T> : IAggregate
{
    T Id { get; }
}
