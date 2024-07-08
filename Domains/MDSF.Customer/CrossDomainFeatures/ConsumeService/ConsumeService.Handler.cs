using Ardalis.GuardClauses;
using MDSF.BuildingBlocks.Dtos;
using MediatR;
using static MDSF.CrossDomains.Tasks.Customer.ConsumeService.ConsumeService;

namespace MDSF.Customer.CrossDomainFeatures.ConsumeService
{
    public static partial class ConsumeService
    {
        private class ApplyServiceHandler(IConsumeServiceAdoContext applyServiceAdoContext)
            : IRequestHandler<ConsumeServiceTask, TResult<ConsumeServiceResponse>>
        {

            public async Task<TResult<ConsumeServiceResponse>> Handle(ConsumeServiceTask request, CancellationToken cancellationToken)
            {

                Guard.Against.Null(request, nameof(ConsumeServiceTask));

                var result = await applyServiceAdoContext.ConsumeService(request.Request.ServiceId, request.Request.ServiceId, CancellationToken.None);

                return new TResult<ConsumeServiceResponse> { Value = new ConsumeServiceResponse() { Result = result } };

            }

        }

    }
}
