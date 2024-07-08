using MediatR;

namespace MDSF.BuildingBlocks.MediatR
{
    public interface IMediatRService
    {
        Task<T> ExecuteAsync<T>(IRequest<T> task, CancellationToken cancellationToken);
    }
}