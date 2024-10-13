using FloodRiskPremiumCalculator.Ui.Models;
using System.Text;
using System.Text.Json;

namespace FloodRiskPremiumCalculator.Ui.Clients;

public interface IBffClientService
{
    Task<IEnumerable<StateModel>> GetStatesAsync(CancellationToken cancellationToken);
    Task<StateRatesViewModel> CalculatePremiumsAsync(CalculatePremiumViewModel data, CancellationToken cancellationToken);
}

public class BffClientService(HttpClient httpClient) : IBffClientService
{
    public async Task<IEnumerable<StateModel>> GetStatesAsync(CancellationToken cancellationToken)
    {
        const string endpoint = "api/states";

        var requestUri = new Uri(httpClient.BaseAddress!, endpoint);

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

        var response = await httpClient.SendAsync(request, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<OperationResult<IEnumerable<StateModel>>>(cancellationToken);

        return result.Result;
    }

    public async Task<StateRatesViewModel> CalculatePremiumsAsync(CalculatePremiumViewModel data, CancellationToken cancellationToken)
    {
        const string endpoint = "api/state/calculate-premium";

        var requestUri = new Uri(httpClient.BaseAddress!, endpoint);

        var payload = JsonSerializer.Serialize(data);

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        };

        var response = await httpClient.SendAsync(request, cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<OperationResult<StateRatesViewModel>>(cancellationToken);

        return result.Result;
    }
}