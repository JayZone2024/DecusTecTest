namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;

public class WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRule : PremiumCalculationBuilderBase
{
    private const decimal SupplementCharge = 50M;
    private const string BandB = "B";

    public WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRule()
    {
        Name = nameof(WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRule);
    }

    public override Task ApplyAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default)
    {
        if (context.State != California)
            return Task.CompletedTask;

        var floridaStateBandARating = context.Ratings.First(_ => _.Band == BandB);
        var floridaBandAPremiumCalculations = context.StatePremiumCalculation.Ratings.First(_ => _.Band == BandB);

        floridaBandAPremiumCalculations.Premium = floridaStateBandARating.BaseRate * context.RiskDistanceFromWater + SupplementCharge;

        return Task.CompletedTask;
    }
}