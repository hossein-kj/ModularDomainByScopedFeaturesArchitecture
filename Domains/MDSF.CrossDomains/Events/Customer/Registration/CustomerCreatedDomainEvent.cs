using MDSF.BuildingBlocks.Domain.Event;

namespace MDSF.CrossDomains.Events.Customer.Registration
{
    public record CustomerCreatedDomainEvent(long Id, bool IsDeleted) : IDomainEvent;
}
