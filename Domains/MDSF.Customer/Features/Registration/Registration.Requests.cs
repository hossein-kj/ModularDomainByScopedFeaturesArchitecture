using MDSF.Customer.MediatR;
using MDSF.Customer.Models.ValueObjects;

namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        public record CreateCustomerRequest(long Id,string Name, CustomerInfo CustomerInfo, Account Account) : ICustomerRequest<CreateCustomerResponse>;
    }
}
