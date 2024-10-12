using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;

namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

public interface IPremiumCalculator
{
    Task<StateRateDto> CalculatePremiumAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default);
}

public class PremiumCalculator : IPremiumCalculator
{
    private readonly IPremiumCalculationRule _premiumCalculationRules = new ApplyDefaultPremiumCalculationRule()
        .AddNextRule(new WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRule())
        .AddNextRule(new WhenStateIsFloridaAndBandIsAAndDistanceToWaterIsLessThanFiveMilesThanApplyMultiplierRule());

    public async Task<StateRateDto> CalculatePremiumAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default)
    {
        await _premiumCalculationRules.ApplyAsync(context);

        return context.StatePremiumCalculation;
    }
}