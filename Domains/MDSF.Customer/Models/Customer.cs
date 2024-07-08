using MDSF.BuildingBlocks.EventStoreDB.Events;
using MDSF.CrossDomains.Events.Customer.Registration;
using MDSF.Customer.Models.ValueObjects;

namespace MDSF.Customer.Models;

internal class Customer : AggregateEventSourcing<long>
{
    public Customer()
    {
    }

    public Account Account { get; private set; }
    public CustomerInfo CustomerInfo { get; private set; }

    public static Customer Create(long id, CustomerInfo customerInfo, Account account, bool isDeleted = false)
    {
        var customer = new Customer()
        {
            Id = id,
            Account = account,
            CustomerInfo = customerInfo,
            IsDeleted = isDeleted
        };

        var @event = new CustomerCreatedDomainEvent(customer.Id, customer.IsDeleted);

        customer.AddDomainEvent(@event);
        customer.Apply(customer);

        return customer;
    }

    private void Apply(Customer customer)
    {
        Id = customer.Id;
        Account = customer.Account;
        CustomerInfo = customer.CustomerInfo;
        IsDeleted = customer.IsDeleted;
        Version++;
    }
}
