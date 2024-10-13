using FloodRiskPremiumCalculator.Ui.Clients;
using FloodRiskPremiumCalculator.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace FloodRiskPremiumCalculator.Ui.Controllers
{
    public class PremiumController(IBffClientService client) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Index(StateViewModel model, CancellationToken cancellationToken)
        {
            var distanceFromWater = decimal.Parse(model.DistanceFromWater);

            var calculatePremiumModel = new CalculatePremiumViewModel { DistanceToWater = distanceFromWater, State = model.SelectedState };

            var result = await client.CalculatePremiumsAsync(calculatePremiumModel, cancellationToken);

            return View(result);
        }
    }
}
