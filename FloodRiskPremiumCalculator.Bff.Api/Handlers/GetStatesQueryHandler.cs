using FloodRiskPremiumCalculator.Bff.Api.Dtos;
using FloodRiskPremiumCalculator.Bff.Api.Repositories;
using MediatR;

namespace FloodRiskPremiumCalculator.Bff.Api.Handlers;

public record GetStatesQuery : IRequest<OperationResult<IEnumerable<StateDto>>>;

public class GetStatesQueryHandler(IStateRatingRepository repository) : IRequestHandler<GetStatesQuery, OperationResult<IEnumerable<StateDto>>>
{
    public async Task<OperationResult<IEnumerable<StateDto>>> Handle(GetStatesQuery request, CancellationToken cancellationToken)
    {
        var results = await repository.GetAllAsync(cancellationToken);

        var states = results.Select(_ => StateDto.CreateWith(_.State));

        return OperationResult<IEnumerable<StateDto>>.OnSuccess(states);
    }
}