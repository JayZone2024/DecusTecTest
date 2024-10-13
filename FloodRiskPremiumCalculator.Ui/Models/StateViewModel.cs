using Microsoft.AspNetCore.Mvc.Rendering;

namespace FloodRiskPremiumCalculator.Ui.Models;

public class StateViewModel
{
    public List<SelectListItem> States { get; set; }

    public string SelectedState { get; set; }

    public string DistanceFromWater { get; set; }

    public static StateViewModel CreateWith(IEnumerable<StateModel> states) => new()
    {
        States = states
            .Select(_ => new SelectListItem { Text = _.State, Value = _.State })
            .ToList()
    };
}

public class StateModel
{
    public string State { get; set; }
}