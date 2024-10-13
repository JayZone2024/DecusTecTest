using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;
using FluentAssertions;

namespace FloodRiskPremiumCalculator.Bff.Api.UnitTests.Rules
{
    public class ApplyDefaultPremiumCalculationRuleTests : TestBase
    {
        private readonly ApplyDefaultPremiumCalculationRule _rule = new();

        [Theory]
        [InlineData(Florida)]
        [InlineData(California)]
        public async Task WhenOnApplyRuleThenBaseRateForStateAndBandShouldApplyCorrectly(string state)
        {
            // Arrange
            PremiumCalculationContext context = new()
            {
                State = state,
                RiskDistanceFromWater = 100M,
                Ratings = BuildFakeRatingModels()
            };

            var expectedStatePremiumCalculation = new StateRateDto
            {
                State = state,
                Ratings = new List<RatingDto>
                {
                    new() { Band = BandA, Premium = 10000 },
                    new() { Band = BandB, Premium = 20000 }
                }
            };

            // Act
            await _rule.ApplyAsync(context, CancellationToken);

            // Assert
            context
                .StatePremiumCalculation
                .Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(expectedStatePremiumCalculation);
        }
    }
}
