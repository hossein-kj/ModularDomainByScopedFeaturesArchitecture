using MDSF.BuildingBlocks.Dtos;
using MediatR;

namespace MDSF.Operator.MediatR
{
    internal interface IOperatorRequest<T> : IRequest<TResult<T>>
    {
    }
}
