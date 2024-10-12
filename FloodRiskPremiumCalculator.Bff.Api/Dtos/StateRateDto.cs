namespace FloodRiskPremiumCalculator.Bff.Api.Dtos;

public class StateRateDto
{
    public string State { get; set; }

    public ICollection<RatingDto> Ratings { get; set; }
}