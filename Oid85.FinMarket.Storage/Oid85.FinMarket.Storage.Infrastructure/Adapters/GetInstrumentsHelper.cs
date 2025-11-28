using NLog;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using Instrument = Oid85.FinMarket.Storage.Core.Models.Instrument;
using TinkoffShare = Tinkoff.InvestApi.V1.Share;
using TinkoffFuture = Tinkoff.InvestApi.V1.Future;
using TinkoffBond = Tinkoff.InvestApi.V1.Bond;

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
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
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

    public async Task<List<Instrument>> GetFuturesAsync()
    {
        try
        {
            await Task.Delay(DelayInMilliseconds);

            List<TinkoffFuture> tinkoffInstruments = (await client.Instruments.FuturesAsync()).Instruments
                .Where(x => x.CountryOfRisk.ToLower() == "ru").ToList();

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
                    Ticker = tinkoffInstrument.Ticker,
                    Name = tinkoffInstrument.Name,
                    Type = KnownInstrumentTypes.Future
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

    public async Task<List<Instrument>> GetBondsAsync()
    {
        try
        {
            await Task.Delay(DelayInMilliseconds);

            List<TinkoffBond> tinkoffInstruments = (await client.Instruments.BondsAsync()).Instruments
                .Where(x => x.CountryOfRisk.ToLower() == "ru")
                .Where(x => x.RiskLevel == RiskLevel.Low || x.RiskLevel == RiskLevel.Moderate)
                .ToList();

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
                    Ticker = tinkoffInstrument.Ticker,
                    Name = tinkoffInstrument.Name,
                    Type = KnownInstrumentTypes.Bond
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

    public async Task<List<Instrument>> GetIndexesAsync()
    {
        try
        {
            await Task.Delay(DelayInMilliseconds);

            var request = new IndicativesRequest();

            var tinkoffInstruments = (await client.Instruments.IndicativesAsync(request)).Instruments.ToList();

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
                    Ticker = tinkoffInstrument.Ticker,
                    Name = tinkoffInstrument.Name,
                    Type = KnownInstrumentTypes.Index
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