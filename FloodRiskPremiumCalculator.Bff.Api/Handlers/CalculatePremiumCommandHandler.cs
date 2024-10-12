using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Extensions;
using FloodRiskPremiumCalculator.Bff.Api.Repositories;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine;
using MediatR;

namespace FloodRiskPremiumCalculator.Bff.Api.Handlers;

public record CalculatePremiumCommand(string State, decimal DistanceToWater) : IRequest<OperationResult<StateRateDto>>;

public class CalculatePremiumCommandHandler(
    IStateRatingRepository repository,
    IPremiumCalculator premiumCalculator) : IRequestHandler<CalculatePremiumCommand, OperationResult<StateRateDto>>
{
    public async Task<OperationResult<StateRateDto>> Handle(CalculatePremiumCommand command, CancellationToken cancellationToken)
    {
        var stateRate = await repository.GetRatingForStateAsync(command.State, cancellationToken);

        if (stateRate.IsNull())
        {
            return OperationResult<StateRateDto>.OnError($"No ratings found for state {command.State}");
        }

        var context = PremiumCalculationContext.CreateWith(
            command.State,
            command.DistanceToWater,
            stateRate.Ratings);

        var result = await premiumCalculator.CalculatePremiumAsync(context, cancellationToken);

        return OperationResult<StateRateDto>.OnSuccess(result);
    }
}