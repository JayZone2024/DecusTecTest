namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

public abstract class PremiumCalculationBuilderBase : IPremiumCalculationRule
{
    protected const string Florida = "Florida";
    protected const string California = "California";

    public string Name { get; protected set; }


    public IPremiumCalculationRule AddNextRule(IPremiumCalculationRule nextRule)
    {
        return new AddNextPremiumCalculationRule(this, nextRule);
    }

    public abstract Task ApplyAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default);
}