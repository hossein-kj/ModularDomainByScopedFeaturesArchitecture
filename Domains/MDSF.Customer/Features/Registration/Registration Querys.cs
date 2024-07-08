using MDSF.BuildingBlocks.Caching;
using MDSF.Customer.MediatR;

namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        internal record GetAvailableRegistrationQuery : ICustomerRequest<GetAvailableRegistrationQueryResponse>, ICacheRequest
        {
            public string CacheKey => "GetAvailableRegistrationQuery";
            public DateTime? AbsoluteExpirationRelativeToNow => DateTime.Now.AddHours(1);
        }
    }
}
