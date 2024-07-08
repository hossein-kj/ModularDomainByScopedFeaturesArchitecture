using MagicOnion;
using MDSF.BuildingBlocks.Dtos;
using static MDSF.CrossDomains.Tasks.Customer.ConsumeService.ConsumeService;

namespace MDSF.CrossDomains.GRPC.Customer.ConsumeService
{
    public interface IConsumeServiceGrpc : IService<IConsumeServiceGrpc>
    {
        UnaryResult<TResult<ConsumeServiceResponse>> ConsumeService(ConsumeServiceRequest consumeServiceRequest);
    }
}
