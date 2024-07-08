using MDSF.BuildingBlocks.Endpoints.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using static MDSF.Customer.Features.Registration.Registration;

namespace MDSF.Customer.Features.Registration
{
    [Route(BaseApiPath + "/Customer/Create")]
    public class CreateCustomerWebEndpoint() : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create new Customer", Description = "Create new Customer")]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await Mediator.ExecuteAsync(new CreateCustomerCommand(request), cancellationToken));
        }
    }

    [Route(BaseApiPath + "/Customer/GetAvailableRegistration")]
    public class GetAvailableRegistrationWebEndpoint() : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Get Available Registration", Description = "Get Available Registration")]
        public async Task<ActionResult> CreateCustomer(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.ExecuteAsync(new GetAvailableRegistrationQuery(), cancellationToken));
        }
    }

    public class PrivacyController : Controller
    {
        private readonly ILogger<PrivacyController> _logger;

        public PrivacyController(ILogger<PrivacyController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
