using MDSF.BuildingBlocks.IdsGenerator;
using MDSF.Customer.MediatR;

namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        internal record CreateCustomerCommand(CreateCustomerRequest Request) : ICustomerRequest<CreateCustomerResponse>
        {
            public long Id { get; set; } = SnowFlakIdGenerator.NewId();
        }
    }
}
