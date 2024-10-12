using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FloodRiskPremiumCalculator.Bff.Api.Controllers;

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

    [HttpPost("state/calculate-premium")]
    public async Task<IActionResult> Post([FromBody] CalculatePremiumDto calculatePremiumDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CalculatePremiumCommand(calculatePremiumDto.State, calculatePremiumDto.DistanceToWater),
            cancellationToken);

        if (result.IsSuccessful)
            return Ok(result);

        return BadRequest(result);
    }
}