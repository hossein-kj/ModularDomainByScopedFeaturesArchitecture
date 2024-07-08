using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.Endpoints.Win;

namespace MDSF.Operator.Features.ApplyService
{
    public static partial class ApplyService
    {
        public class ApplyServiceWinEndpoint : BaseWinEndpoint
        {
            public async Task<TResult<ApplyServiceResponse>> ApplyService(ApplyServiceRequest request,
                CancellationToken cancellationToken)
            {

                var result = await Mediator.ExecuteAsync(new ApplyServiceCommand(new ApplyServiceRequest(request.serviceId, request.userId))
                , cancellationToken);
                return new TResult<ApplyServiceResponse>
                {
                    Value = new ApplyServiceResponse(result.Value.Result),
                    Message = result.Message,
                    StatusCode = result.StatusCode
                };
            }
        }
    }
}
