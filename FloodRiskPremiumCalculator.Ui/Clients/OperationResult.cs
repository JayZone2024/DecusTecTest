namespace FloodRiskPremiumCalculator.Ui.Clients;

public class OperationResult<TResult>
{
    public bool IsSuccessful { get; set; }

    public TResult Result { get; set; }

    public string Error { get; set; }
}