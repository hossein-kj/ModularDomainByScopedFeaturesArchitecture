using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.Endpoints.Win;

namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        public class CreateCustomerWinEndpoint : BaseWinEndpoint
        {
            public async Task<TResult<CreateCustomerResponse>> CreateCustomer(CreateCustomerRequest request,
                CancellationToken cancellationToken)
            {

                return (await Mediator.ExecuteAsync(new CreateCustomerCommand(request), cancellationToken));
            }
        }
    }
}
