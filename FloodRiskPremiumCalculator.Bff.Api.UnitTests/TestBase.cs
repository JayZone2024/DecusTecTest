using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Repositories.Models;

namespace FloodRiskPremiumCalculator.Bff.Api.UnitTests
{
    public abstract class TestBase
    {
        protected const string Florida = "Florida";
        protected const string California = "California";
        protected const string BandA = "A";
        protected const string BandB = "B";

        protected CancellationToken CancellationToken => default;

        protected static ICollection<RatingModel> BuildFakeRatingModels() => new List<RatingModel>
        {
            new RatingModel { Band = BandA, BaseRate = 100 },
            new RatingModel { Band = BandB, BaseRate = 200 }
        };

        protected static IEnumerable<StateRateModel> BuildFakeStateRateModels()
        {
            return new List<StateRateModel>
            {
                new()
                {
                    State = "California", Ratings = new List<RatingModel>
                    {
                        new() { Band = "A", BaseRate = 100 },
                        new() { Band = "B", BaseRate = 200 },
                    }
                },

                new()
                {
                    State = "Florida", Ratings = new List<RatingModel>
                    {
                        new() { Band = "A", BaseRate = 300 },
                        new() { Band = "B", BaseRate = 400 },
                    }
                }
            };
        }
    }
}
