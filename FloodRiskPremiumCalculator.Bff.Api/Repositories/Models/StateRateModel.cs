namespace FloodRiskPremiumCalculator.Bff.Api.Repositories.Models
{
    public class StateRateModel
    {
        public string State { get; set; }

        public ICollection<RatingModel> Ratings { get; set; }
    }
}
