namespace FloodRiskPremiumCalculator.Bff.Api.Dtos;

public class CalculatePremiumDto
{
    public string State { get; set; }

    public decimal DistanceToWater { get; set; }
}