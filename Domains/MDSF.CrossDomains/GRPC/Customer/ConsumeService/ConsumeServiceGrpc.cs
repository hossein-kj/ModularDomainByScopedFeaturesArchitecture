using MagicOnion;
using MagicOnion.Server;
using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.MediatR;
using static MDSF.CrossDomains.Tasks.Customer.ConsumeService.ConsumeService;

namespace MDSF.CrossDomains.GRPC.Customer.ConsumeService
{
    public class ConsumeServiceGrpc(IMediatRService mediatRService) : ServiceBase<IConsumeServiceGrpc>, IConsumeServiceGrpc
    {
        public async UnaryResult<TResult<ConsumeServiceResponse>> ConsumeService(ConsumeServiceRequest consumeServiceRequest)
        {
            return await mediatRService.ExecuteAsync(new ConsumeServiceTask(consumeServiceRequest)
                , CancellationToken.None);

        }


    }
}
