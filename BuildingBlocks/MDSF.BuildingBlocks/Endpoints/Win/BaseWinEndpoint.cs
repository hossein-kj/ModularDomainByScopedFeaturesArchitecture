using MDSF.BuildingBlocks.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MDSF.BuildingBlocks.Endpoints.Win
{
    public abstract class BaseWinEndpoint
    {
        private IMediatRService _mediator;

        protected IMediatRService Mediator =>
            _mediator ??= WindowsServiceCollectionManager.ServiceProvider.GetRequiredService<IMediatRService>();
    }
}
