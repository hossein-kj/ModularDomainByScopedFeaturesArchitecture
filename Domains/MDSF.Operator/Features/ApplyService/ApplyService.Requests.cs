namespace MDSF.Operator.Features.ApplyService
{
    public static partial class ApplyService
    {
        public record ApplyServiceRequest(long serviceId, long userId);
    }
}
