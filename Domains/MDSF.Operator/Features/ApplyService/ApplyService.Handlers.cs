using Ardalis.GuardClauses;
using Grpc.Net.Client;
using MagicOnion.Client;
using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.Globalization;
using MDSF.BuildingBlocks.MediatR;
using MDSF.BuildingBlocks.Options;
using MDSF.CrossDomains.GRPC;
using MDSF.CrossDomains.GRPC.Customer.ConsumeService;
using MediatR;
using Microsoft.Extensions.Options;
using static MDSF.CrossDomains.Tasks.Customer.ConsumeService.ConsumeService;

namespace MDSF.Operator.Features.ApplyService
{
    public static partial class ApplyService
    {
        private class ApplyServiceCommandHandler(
            ITextResource textResource, IMediatRService mediatRService
            , IOptions<GrpcOptions> grpcOptions, IOptions<AppOptions> appOption)
            : IRequestHandler<ApplyServiceCommand, TResult<ApplyServiceResponse>>
        {
            public async Task<TResult<ApplyServiceResponse>>
                Handle(ApplyServiceCommand request,
                CancellationToken cancellationToken)
            {
                Guard.Against.Null(request, nameof(request));


                var resultConsumeService = new TResult<ConsumeServiceResponse>();

                if (appOption.Value.CustomerIsMicroService)
                {
                    resultConsumeService = await CallMicroService(request, cancellationToken);
                }
                else
                {
                    resultConsumeService = await CallMonolit(request, cancellationToken);
                }

                var result = new TResult<ApplyServiceResponse>
                {
                    Value = new ApplyServiceResponse(true)
                };

                if (result.StatusCode != 200)
                {
                    result.Value = new ApplyServiceResponse(resultConsumeService.Value.Result);
                    result.Message = resultConsumeService.Message;
                    result.StatusCode = resultConsumeService.StatusCode;
                }
                return result;

            }

            private async Task<TResult<ConsumeServiceResponse>> CallMonolit(ApplyServiceCommand request, CancellationToken cancellationToken)
            {
                return await mediatRService.ExecuteAsync(new ConsumeServiceTask(new ConsumeServiceRequest()
                {
                    ServiceId = request.Request.serviceId,
                    UserId = request.Request.userId
                })
               , cancellationToken);
            }

            private async Task<TResult<ConsumeServiceResponse>> CallMicroService(ApplyServiceCommand request, CancellationToken cancellationToken)
            {
                    var channelConsumeService = GrpcChannel.ForAddress(grpcOptions.Value.CustomerAddress);
                    var consumeServiceGrpc = new Lazy<IConsumeServiceGrpc>(() => MagicOnionClient.Create<IConsumeServiceGrpc>(channelConsumeService)).Value;

                    return await consumeServiceGrpc.ConsumeService(new ConsumeServiceRequest()
                    {
                        ServiceId = request.Request.serviceId,
                        UserId = request.Request.userId
                    });
            }
        }

    }
}
