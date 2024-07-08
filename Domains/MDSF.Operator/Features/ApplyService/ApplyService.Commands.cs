using MDSF.BuildingBlocks.Dtos;
using MDSF.Operator.MediatR;

namespace MDSF.Operator.Features.ApplyService
{
    public static partial class ApplyService
    {
        internal record ApplyServiceCommand(ApplyServiceRequest Request) : IOperatorRequest<ApplyServiceResponse>;
    }
}
