using MDSF.BuildingBlocks.Dtos;
using MDSF.BuildingBlocks.Endpoints.Web;
using MDSF.CrossDomains.GRPC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using static MDSF.Operator.Features.ApplyService.ApplyService;

namespace MDSF.Operator.Features.ApplyService
{
    [Route(BaseApiPath + "/Operator/ApplyService")]
    public class ApplyServiceWebEndpoint(IOptions<GrpcOptions> grpcOptions) : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Apply a Service", Description = "Apply a Service")]
        public async Task<ActionResult> ApplyService([FromBody] ApplyServiceRequest request,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.ExecuteAsync(new ApplyServiceCommand(new ApplyServiceRequest(request.serviceId, request.userId))
       , cancellationToken);


            return Ok(new TResult<ApplyServiceResponse>
            {
                Value = new ApplyServiceResponse(result.Value.Result),
                Message = result.Message,
                StatusCode = result.StatusCode
            });
        }
    }
}
