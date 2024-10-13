using FloodRiskPremiumCalculator.Ui.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FloodRiskPremiumCalculator.Ui.Clients;

namespace FloodRiskPremiumCalculator.Ui.Controllers
{
    public class HomeController(IBffClientService client) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var statesData = await client.GetStatesAsync(cancellationToken);

            var viewModel = StateViewModel.CreateWith(statesData);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
