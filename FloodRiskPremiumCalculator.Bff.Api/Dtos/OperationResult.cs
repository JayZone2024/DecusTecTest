using MediatR;

namespace FloodRiskPremiumCalculator.Bff.Api.Dtos;

public class OperationResult<TResult> : IRequest
{
    public bool IsSuccessful { get; set; }

    public TResult Result { get; set; }

    public string Error { get; set; }

    public static OperationResult<TResult> OnSuccess(TResult value) => new() { IsSuccessful = true, Result = value };

    public static OperationResult<TResult> OnError(string error)=> new() { Error = error, IsSuccessful = false };
}