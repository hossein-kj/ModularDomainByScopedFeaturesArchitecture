using MDSF.BuildingBlocks.Dtos;
using MediatR;

namespace MDSF.BuildingBlocks.MediatR
{
    public interface ITask<T> : IRequest<TResult<T>>
    {
    }
}
