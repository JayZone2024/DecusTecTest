namespace FloodRiskPremiumCalculator.Ui.Models
{
    public class StateRatesViewModel
    {
        public string State { get; set; }

        public ICollection<RatingModel> Ratings { get; set; }
    }

    public class RatingModel
    {
        public string Band { get; set; }

        public decimal Premium { get; set; }
    }
}
