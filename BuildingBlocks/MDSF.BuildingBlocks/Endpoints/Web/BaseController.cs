using MDSF.BuildingBlocks.MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MDSF.BuildingBlocks.Endpoints.Web;

[Route(BaseApiPath)]
[ApiController]
[ApiVersion("1.0")]
public abstract class BaseController : ControllerBase
{
    protected const string BaseApiPath = "api/v{version:apiVersion}";

    private IMediatRService _mediator;

    protected IMediatRService Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediatRService>();

}
