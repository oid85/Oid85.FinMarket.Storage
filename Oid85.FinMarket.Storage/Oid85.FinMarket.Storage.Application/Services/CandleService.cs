using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class CandleService (
        IInvestApiClientAdapter investApiClientAdapter,
        IInstrumentRepository instrumentRepository,
        ICandleRepository candleRepository)
        : ICandleService
    {
        public async Task LoadCandlesAsync()
        {
            var instruments = await instrumentRepository.GetActiveInstrumentsAsync();

            if (instruments is null)
                return;

            foreach (var instrument in instruments)
            {
                var lastCandle = await candleRepository.GetLastCandleByTickerAsync(instrument.Ticker);

                if (lastCandle is null)
                {
                    var currentYear = DateTime.Today.Year;

                    for (int i = 5; i >= 0; i--)
                    {
                        var from = DateOnly.FromDateTime(new DateTime(currentYear - i, 1, 1));
                        var to = DateOnly.FromDateTime(new DateTime(currentYear - i, 12, 31));
                        var candles = await investApiClientAdapter.GetCandleAsync(instrument.InstrumentId, from, to);
                        
                        candles.ForEach(x => x.Ticker = instrument.Ticker);
                        candles.ForEach(x => x.Ticks = x.Date.ToDateTime(TimeOnly.MinValue).Ticks);

                        await candleRepository.AddForceAsync(candles);
                    }
                }

                else
                {
                    var from = lastCandle.Date;
                    var to = DateOnly.FromDateTime(DateTime.Today);
                    var candles = await investApiClientAdapter.GetCandleAsync(instrument.InstrumentId, from, to);

                    candles.ForEach(x => x.Ticker = instrument.Ticker);
                    candles.ForEach(x => x.Ticks = x.Date.ToDateTime(TimeOnly.MinValue).Ticks);

                    await candleRepository.AddAsync(candles);
                }
            }
        }
    }
}
