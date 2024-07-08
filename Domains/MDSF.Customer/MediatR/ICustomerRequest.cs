using MDSF.BuildingBlocks.Dtos;
using MediatR;

namespace MDSF.Customer.MediatR
{
    internal interface ICustomerRequest<T> : IRequest<TResult<T>>
    {
    }
}
