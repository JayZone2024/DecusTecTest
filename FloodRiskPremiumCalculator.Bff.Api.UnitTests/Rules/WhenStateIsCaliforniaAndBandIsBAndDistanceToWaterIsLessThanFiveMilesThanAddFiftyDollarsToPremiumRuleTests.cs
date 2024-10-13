using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine;
using FloodRiskPremiumCalculator.Bff.Api.RulesEngine.Rules;
using FluentAssertions;

namespace FloodRiskPremiumCalculator.Bff.Api.UnitTests.Rules
{
    public class WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRuleTests : TestBase
    {
        private readonly WhenStateIsCaliforniaAndBandIsBAndDistanceToWaterIsLessThanFiveMilesThanAddFiftyDollarsToPremiumRule _rule = new();

        [Fact]
        public async Task WhenOnApplyRuleThenBaseRateForStateAndBandShouldApplyCorrectly()
        {
            // Arrange
            PremiumCalculationContext context = new()
            {
                State = California,
                RiskDistanceFromWater = 100M,
                Ratings = BuildFakeRatingModels()
            };

            var expectedStatePremiumCalculation = new StateRateDto
            {
                State = California,
                Ratings = new List<RatingDto>
                {
                    new() { Band = BandA, Premium = 10000 },
                    new() { Band = BandB, Premium = 20050M }
                }
            };

            // Act
            await (new ApplyDefaultPremiumCalculationRule()).ApplyAsync(context, CancellationToken);
            await _rule.ApplyAsync(context, CancellationToken);

            // Assert
            context
                .StatePremiumCalculation
                .Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(expectedStatePremiumCalculation);
        }


        [Fact]
        public async Task WhenOnApplyRuleAndStateIsNotCaliforniaThenRuleShouldNotApply()
        {
            // Arrange
            PremiumCalculationContext context = new()
            {
                State = Florida,
                RiskDistanceFromWater = 100M,
                Ratings = BuildFakeRatingModels()
            };

            var expectedStatePremiumCalculation = new StateRateDto
            {
                State = Florida,
                Ratings = new List<RatingDto>
                {
                    new() { Band = BandA, Premium = 10000 },
                    new() { Band = BandB, Premium = 20000M }
                }
            };

            // Act
            await (new ApplyDefaultPremiumCalculationRule()).ApplyAsync(context, CancellationToken);
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
