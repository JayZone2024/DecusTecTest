using FloodRiskPremiumCalculator.Bff.Api.Dtos;

namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;

public class WhenStateIsFloridaAndBandIsAAndDistanceToWaterIsLessThanFiveMilesThanApplyMultiplierRule : PremiumCalculationBuilderBase
{
    private const decimal Multiplier = 1.2M;
    private const string BandA = "A";

    public WhenStateIsFloridaAndBandIsAAndDistanceToWaterIsLessThanFiveMilesThanApplyMultiplierRule()
    {
        Name = nameof(WhenStateIsFloridaAndBandIsAAndDistanceToWaterIsLessThanFiveMilesThanApplyMultiplierRule);
    }

    public override Task ApplyAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default)
    {
        if (context.State != Florida)
            return Task.CompletedTask;

        var floridaStateBandARating = context.Ratings.First(_ => _.Band == BandA);
        var floridaBandAPremiumCalculations = context.StatePremiumCalculation.Ratings.First(_ => _.Band == BandA);

        floridaBandAPremiumCalculations.Premium = floridaStateBandARating.BaseRate * context.RiskDistanceFromWater * Multiplier;

        return Task.CompletedTask;
    }
}