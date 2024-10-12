using FloodRiskPremiumCalculator.Bff.Api.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FloodRiskPremiumCalculator.Bff.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class StateController(IMediator mediator) : ControllerBase
    {
        [HttpGet("states")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetStatesQuery(), cancellationToken);

            return Ok(result);
        }
    }
}
