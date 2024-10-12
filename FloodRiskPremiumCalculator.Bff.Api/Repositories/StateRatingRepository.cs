﻿using FloodRiskPremiumCalculator.Bff.Api.Extensions;
using FloodRiskPremiumCalculator.Bff.Api.Repositories.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FloodRiskPremiumCalculator.Bff.Api.Repositories;

public interface IStateRatingRepository
{
    Task<IEnumerable<StateRateModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StateRateModel> GetRatingForStateAsync(string state, CancellationToken cancellationToken = default);
}

public class StateRatingRepository : IStateRatingRepository
{
    private const string CacheKey = "StateRatings";

    private readonly IMemoryCache _memoryCache;

    public StateRatingRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;

        Seed();
    }

    public Task<IEnumerable<StateRateModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _memoryCache.TryGetValue(CacheKey, out IEnumerable<StateRateModel> stateRatings);

        return Task.FromResult(stateRatings);
    }

    public Task<StateRateModel> GetRatingForStateAsync(string state, CancellationToken cancellationToken = default)
    {
        _memoryCache.TryGetValue(CacheKey, out IEnumerable<StateRateModel> stateRatings);

        var stateRate = stateRatings.FirstOrDefault(_ => _.State.Equals(state, StringComparison.CurrentCultureIgnoreCase));

        if (stateRate.IsNull())
        {
            return null;
        }

        return Task.FromResult(stateRate);
    }

    private void Seed()
    {
        var stateRatings = new List<StateRateModel>
        {
            new StateRateModel
            {
                State = "California", Ratings = new List<RatingModel>
                {
                    new RatingModel
                        { Band = "A", BaseRate = 600, MaxDistanceApplicableToBaseRate = 5M, Multiplier = 1.2M },
                    new RatingModel
                        { Band = "B", BaseRate = 800, MaxDistanceApplicableToBaseRate = 5M, Multiplier = 1.2M },
                }
            },

            new StateRateModel
            {
                State = "Florida", Ratings = new List<RatingModel>
                {
                    new RatingModel
                        { Band = "A", BaseRate = 500, MaxDistanceApplicableToBaseRate = 5M, Multiplier = 1.2M },
                    new RatingModel
                        { Band = "B", BaseRate = 400, MaxDistanceApplicableToBaseRate = 5M, Multiplier = 1.2M },
                }
            },
        };

        _memoryCache.Set(CacheKey, stateRatings);
    }
}