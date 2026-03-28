using NLog;
using Oid85.FinMarket.Storage.Core.Models;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetDividendInfoHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int DelayInMilliseconds = 1000;
    
    public async Task<List<DividendInfo>> GetDividendInfoAsync(List<Core.Models.Instrument> instruments)
    {
        await Task.Delay(DelayInMilliseconds);
        
        var dividendInfos = new List<DividendInfo>();

        foreach (var instrument in instruments)
        {
            await Task.Delay(DelayInMilliseconds);

            var request = CreateGetDividendsRequest(instrument.InstrumentId);
            var response = await SendGetDividendsRequest(request);

            if (response is null)
                continue;

            if (response.Dividends is not null)
                foreach (var dividend in response.Dividends)
                    if (dividend is not null)
                    {
                        var dividendInfo = new DividendInfo
                        {
                            Ticker = instrument.Ticker,
                            InstrumentId = instrument.InstrumentId,
                            DeclaredDate = ConvertHelper.TimestampToDateOnly(dividend.DeclaredDate),
                            RecordDate = ConvertHelper.TimestampToDateOnly(dividend.RecordDate),
                            Dividend = ConvertHelper.MoneyValueToDouble(dividend.DividendNet),
                            DividendPrc = ConvertHelper.QuotationToDouble(dividend.YieldValue)
                        };

                        dividendInfos.Add(dividendInfo);   
                    }
        }

        return dividendInfos;
    }
    
    private static GetDividendsRequest CreateGetDividendsRequest(Guid instrumentId) =>
        new()
        {
            InstrumentId = instrumentId.ToString(),
            From = ConvertHelper.DateTimeToTimestamp(DateTime.Today),
            To = ConvertHelper.DateTimeToTimestamp(DateTime.Today.AddYears(1))
        };
    
    private async Task<GetDividendsResponse?> SendGetDividendsRequest(GetDividendsRequest request)
    {
        try
        {
            return await client.Instruments.GetDividendsAsync(request);
        }
        
        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных. {request}", request);
            return null;
        }
    }
}