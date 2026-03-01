using Google.Protobuf.WellKnownTypes;
using NLog;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using Candle = Oid85.FinMarket.Storage.Core.Models.Candle;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetCandlesHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int DelayInMilliseconds = 1000;
    
    public Task<List<Candle>> GetCandlesAsync(Guid instrumentId, DateOnly from, DateOnly to) =>
        GetCandlesAsync(instrumentId, ConvertHelper.DateOnlyToTimestamp(from), ConvertHelper.DateOnlyToTimestamp(to));  

    private async Task<List<Candle>> GetCandlesAsync(
        Guid instrumentId, Timestamp from, Timestamp to)
    {
        await Task.Delay(DelayInMilliseconds);
        
        var request = CreateGetCandlesRequest(instrumentId, from, to, CandleInterval.Day);
        var response = await SendGetCandlesRequest(request);

        if (response is null)
            return [];

        if (response.Candles is null)
            return [];

        var candles = response.Candles
            .Where(x => x.IsComplete)
            .Select(x => 
                new Candle
                {
                    Open = ConvertHelper.QuotationToDouble(x.Open),
                    Close = ConvertHelper.QuotationToDouble(x.Close),
                    Low = ConvertHelper.QuotationToDouble(x.Low),
                    High = ConvertHelper.QuotationToDouble(x.High),
                    Volume = x.Volume,
                    Date = ConvertHelper.TimestampToDateOnly(x.Time)
                })
            .ToList();

        return candles;
    }
    private async Task<GetCandlesResponse?> SendGetCandlesRequest(GetCandlesRequest request)
    {
        try
        {
            return await client.MarketData.GetCandlesAsync(request);
        }
        
        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных. {request}", request);
            return null;
        }
    }

    private static GetCandlesRequest CreateGetCandlesRequest(
        Guid instrumentId, Timestamp from, Timestamp to, CandleInterval interval) =>
        new()
        {
            InstrumentId = instrumentId.ToString(),
            From = from,
            To = to,
            Interval = interval
        };
}