using FloodRiskPremiumCalculator.Bff.Api.Repositories.Models;

namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

public interface IPremiumCalculationRule
{
    public string Name { get; }

    IPremiumCalculationRule AddNextRule(IPremiumCalculationRule nextRule);

    Task ApplyAsync(PremiumCalculationContext context);
}