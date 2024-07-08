using MDSF.BuildingBlocks.MediatR;

namespace MDSF.CrossDomains.Tasks.Customer.ConsumeService;

public static partial class ConsumeService
{
    public record ConsumeServiceTask(ConsumeServiceRequest Request) : ITask<ConsumeServiceResponse>;
}
