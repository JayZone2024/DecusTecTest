namespace FloodRiskPremiumCalculator.Bff.Api.Dtos;

public class StateDto
{
    public string State { get; set; }

    public static StateDto CreateWith(string state) => new() { State = state };
}