using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Repositories.Models;

namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

public class PremiumCalculationContext
{
    public string State { get; set; }

    public decimal RiskDistanceFromWater { get; set; }

    public ICollection<RatingModel> Ratings { get; set; }

    public StateRateDto StatePremiumCalculation { get; set; }

    public static PremiumCalculationContext CreateWith(
        string state,
        decimal riskDistanceFromWater,
        ICollection<RatingModel> ratings) => new()
    {
        State = state,
        RiskDistanceFromWater = riskDistanceFromWater,
        Ratings = ratings
    };
}