using Google.Protobuf.WellKnownTypes;
using NLog;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using Instrument = Oid85.FinMarket.Storage.Core.Models.Instrument;
using TinkoffBond = Tinkoff.InvestApi.V1.Bond;
using TinkoffFuture = Tinkoff.InvestApi.V1.Future;
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
                .Where(x => x.CountryOfRisk.Equals(KnownCountryCodes.Ru, StringComparison.OrdinalIgnoreCase)).ToList(); 

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
                    Ticker = tinkoffInstrument.Ticker,                    
                    Name = tinkoffInstrument.Name,
                    Sector = tinkoffInstrument.Sector,
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
                .Where(x => x.CountryOfRisk.Equals(KnownCountryCodes.Ru, StringComparison.OrdinalIgnoreCase)).ToList();

            var instruments = new List<Instrument>();

            foreach (var tinkoffInstrument in tinkoffInstruments)
            {
                var instrument = new Instrument
                {
                    InstrumentId = Guid.Parse(tinkoffInstrument.Uid),
                    Ticker = tinkoffInstrument.Ticker,
                    Name = tinkoffInstrument.Name,
                    Sector = tinkoffInstrument.Sector,
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
                .Where(x => x.CountryOfRisk.Equals(KnownCountryCodes.Ru, StringComparison.OrdinalIgnoreCase))
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
                    MaturityDate = TimestampToDateOnly(tinkoffInstrument.MaturityDate),
                    CouponQuantityPerYear = tinkoffInstrument.CouponQuantityPerYear,
                    Nkd = MoneyValueToDouble(tinkoffInstrument.AciValue),
                    Sector = tinkoffInstrument.Sector,
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

    private static DateOnly TimestampToDateOnly(Timestamp timestamp) =>
        timestamp is null ? DateOnly.MinValue : DateOnly.FromDateTime(timestamp.ToDateTime());

    private static double MoneyValueToDouble(MoneyValue moneyValue)
    {
        if (moneyValue is null)
            return 0.0;

        return moneyValue.Units + moneyValue.Nano / 1_000_000_000.0;
    }
}