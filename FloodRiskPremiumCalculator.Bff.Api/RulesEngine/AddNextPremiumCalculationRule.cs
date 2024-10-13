namespace FloodRiskPremiumCalculator.Bff.Api.RulesEngine;

public class AddNextPremiumCalculationRule : PremiumCalculationBuilderBase
{
    private readonly IPremiumCalculationRule _nextRule;
    private readonly IPremiumCalculationRule _previousRule;

    public AddNextPremiumCalculationRule(IPremiumCalculationRule previousRule, IPremiumCalculationRule nextRule)
    {
        _previousRule = previousRule;
        _nextRule = nextRule;

        Name = nameof(AddNextRule);
    }

    public override async Task ApplyAsync(PremiumCalculationContext context, CancellationToken cancellationToken = default)
    {
        await _previousRule.ApplyAsync(context, cancellationToken);
        await _nextRule.ApplyAsync(context, cancellationToken);
    }
}