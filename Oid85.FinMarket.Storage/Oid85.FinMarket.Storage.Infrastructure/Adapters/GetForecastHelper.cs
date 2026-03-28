using NLog;
using Oid85.FinMarket.Storage.Core.Models;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetForecastHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int DelayInMilliseconds = 1000;

    public async Task<List<ForecastConsensus>> GetForecastConsensusesAsync(List<Core.Models.Instrument> instruments)
    {
        var consensuses = new List<ForecastConsensus>();

        foreach (var instrument in instruments)
        {
            var consensus = await GetForecastConsensusAsync(instrument.InstrumentId);

            if (consensus is not null)
                consensuses.Add(consensus);
        }

        return consensuses;
    }

    public async Task<ForecastConsensus?> GetForecastConsensusAsync(Guid instrumentId)
    {
        await Task.Delay(DelayInMilliseconds);

        var request = CreateGetForecastRequest(instrumentId);
        var response = await SendGetForecastRequest(request);
        
        if (response is null)
            return null;

        var consensus = response.Consensus is null 
            ? null
            : new ForecastConsensus
            {
                InstrumentId = Guid.Parse(response.Consensus.Uid),
                Ticker = response.Consensus.Ticker,
                Currency = response.Consensus.Currency,
                CurrentPrice = ConvertHelper.QuotationToDouble(response.Consensus.CurrentPrice),
                ConsensusPrice = ConvertHelper.QuotationToDouble(response.Consensus.Consensus),
                MinTarget = ConvertHelper.QuotationToDouble(response.Consensus.MinTarget),
                MaxTarget = ConvertHelper.QuotationToDouble(response.Consensus.MaxTarget),
                PriceChange = ConvertHelper.QuotationToDouble(response.Consensus.PriceChange),
                PriceChangeRel = ConvertHelper.QuotationToDouble(response.Consensus.PriceChangeRel),
                RecommendationNumber = response.Consensus.Recommendation switch
                {
                    Recommendation.Buy => 1,
                    Recommendation.Hold => 2,
                    Recommendation.Sell => 3,
                    _ => 0
                },
                RecommendationString = response.Consensus.Recommendation switch
                {
                    Recommendation.Buy => "Buy",
                    Recommendation.Hold => "Hold",
                    Recommendation.Sell => "Sell",
                    _ => string.Empty
                }
            };
        
        return consensus;
    }
        
    private static GetForecastRequest CreateGetForecastRequest(Guid instrumentId) =>
        new()
        {
            InstrumentId = instrumentId.ToString()
        };
    
    private async Task<GetForecastResponse?> SendGetForecastRequest(GetForecastRequest request)
    {
        try
        {
            return await client.Instruments.GetForecastByAsync(request);
        }
        
        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных. {request}", request);
            return null;
        }
    }
}