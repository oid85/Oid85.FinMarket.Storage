using NLog;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Tinkoff.InvestApi;
using Instrument = Oid85.FinMarket.Storage.Core.Models.Instrument;
using TinkoffShare = Tinkoff.InvestApi.V1.Share;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters;

public class GetInstrumentsHelper(
    ILogger logger,
    InvestApiClient client)
{
    private const int DelayInMilliseconds = 1000;
    
    public async Task<List<Instrument>> GetSharesAsync()
    {
        try
        {
            await Task.Delay(DelayInMilliseconds);
            
            List<TinkoffShare> tinkoffInstruments = (await client.Instruments.SharesAsync()).Instruments
                .Where(x => x.CountryOfRisk.ToLower() == "ru").ToList(); 

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    Ticker = tinkoffInstrument.Ticker,
                    Name = tinkoffInstrument.Name,
                    Type = KnownInstrumentTypes.Share
                };

                instruments.Add(instrument);
            }

            return instruments;
        }

        catch (Exception exception)
        {
            logger.Error(exception, "Ошибка получения данных");
            return [];
        }
    }   
}