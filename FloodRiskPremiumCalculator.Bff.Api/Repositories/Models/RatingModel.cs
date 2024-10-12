namespace FloodRiskPremiumCalculator.Bff.Api.Repositories.Models;

public class RatingModel
{
    public string Band { get; set; }

    public int BaseRate { get; set; }

    public decimal Multiplier { get; set; }

    public decimal MaxDistanceApplicableToBaseRate { get; set; }
}