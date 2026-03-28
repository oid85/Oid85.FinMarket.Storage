using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class ForecastService(
        IInstrumentRepository instrumentRepository,
        IInvestApiClientAdapter investApiClientAdapter,
        IForecastRepository forecastRepository)
        : IForecastService
    {
        public async Task<GetForecastListResponse> GetForecastListAsync(GetForecastListRequest request)
        {
            var forecasts = await forecastRepository.GetForecastsAsync();

            if (forecasts is null)
                return new GetForecastListResponse { Forecasts = [] };

            return new GetForecastListResponse
            {
                Forecasts = forecasts
                .Select(x =>
                new GetForecastListItemResponse
                {
                    Ticker = x.Ticker,
                    CurrentPrice = x.CurrentPrice,
                    ConsensusPrice = x.ConsensusPrice,
                    MinTarget = x.MinTarget,
                    MaxTarget = x.MaxTarget,
                    PriceChange = x.PriceChange,
                    PriceChangeRel = x.PriceChangeRel,
                    RecommendationString = x.RecommendationString
                })
                .ToList()
            };
        }

        public async Task LoadForecastConsensusesAsync()
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync())?
                .Where(x => x.Type == KnownInstrumentTypes.Share)
                .ToList();

            if (instruments is null)
                return;

            var forecasts = await investApiClientAdapter.GetForecastConsensusesAsync(instruments);

            await forecastRepository.AddAsync(forecasts);
        }
    }
}
