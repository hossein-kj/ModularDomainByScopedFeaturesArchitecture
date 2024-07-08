using Ardalis.GuardClauses;
using DotNetCore.CAP;
using MDSF.CrossDomains.Events.Customer.Registration;
using static MDSF.BuildingBlocks.CAP.Extensions;

namespace MDSF.Operator.CrossDomainFeatures.ConfirmRegistration
{
    public static partial class ConfirmRegistration
    {
        private class ConfirmRegisterationHandler
            (IConfirmRegistrationRegistrationAdoContext confirmRegisterationRegistrationAdoContext) : ICapSubscribe
        {

            [CapSubscribe(nameof(CustomerCreatedDomainEvent) + "." + nameof(CapBusName.Monolit))]
            internal async Task Consume(CustomerCreatedDomainEvent customerCreatedDomain)
            {
                Guard.Against.Null(customerCreatedDomain, nameof(CustomerCreatedDomainEvent));

            
                    await confirmRegisterationRegistrationAdoContext.ConfirmRegisteration(customerCreatedDomain.Id, CancellationToken.None);
       

            }
        }
    }
}
