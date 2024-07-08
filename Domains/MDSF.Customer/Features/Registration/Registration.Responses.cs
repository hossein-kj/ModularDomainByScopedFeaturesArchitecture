namespace MDSF.Customer.Features.Registration
{
    public static partial class Registration
    {
        public record CreateCustomerResponse(long CustomerId);
        public record GetAvailableRegistrationQueryResponse(long CustomerId);
    }
}
