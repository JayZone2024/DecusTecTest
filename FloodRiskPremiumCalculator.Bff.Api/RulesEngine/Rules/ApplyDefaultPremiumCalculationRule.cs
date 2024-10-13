using FloodRiskPremiumCalculator.Bff.Api.Dtos;

namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;

public class ApplyDefaultPremiumCalculationRule : PremiumCalculationBuilderBase
{
    public ApplyDefaultPremiumCalculationRule()
    {
        Name = nameof(ApplyDefaultPremiumCalculationRule);
    }

    public override Task ApplyAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default)
    {
        context.StatePremiumCalculation = new StateRateDto
        {
            State = context.State,
            Ratings = new List<RatingDto>()
        };

        foreach (var contextRating in context.Ratings)
        {
            context
                .StatePremiumCalculation
                .Ratings
                .Add(new RatingDto { Band = contextRating.Band, Premium = contextRating.BaseRate * context.RiskDistanceFromWater });
        }

        return Task.CompletedTask;
    }
}