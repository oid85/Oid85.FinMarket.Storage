using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc />
    public class CandleService (
        IInvestApiClientAdapter investApiClientAdapter,
        IInstrumentRepository instrumentRepository,
        ICandleRepository candleRepository)
        : ICandleService
    {
        /// <inheritdoc />
        public async Task<GetCandleListResponse> GetCandleListAsync(GetCandleListRequest request)
        {
            var candles = await candleRepository.GetCandlesAsync(request.Ticker, request.From, request.To);

            if (candles is null)
                return new GetCandleListResponse { Candles = [] };

            return new GetCandleListResponse 
            { 
                Candles = candles
                .Select(x =>
                new GetCandleListItemResponse
                {
                    Open = x.Open,
                    Close = x.Close,
                    Low = x.Low,
                    High = x.High,
                    Date = x.Date,
                    Volume = x.Volume
                })
                .ToList()
            };
        }

        /// <inheritdoc />
        public async Task<GetLastCandleResponse> GetLastCandleAsync(GetLastCandleRequest request)
        {
            var response = new GetLastCandleResponse();

            foreach (var ticker in request.Tickers)
            {
                var candle = await candleRepository.GetLastCandleAsync(ticker, request.Date);

                if (candle is null)
                    response.Candles.Add(null);

                else
                    response.Candles.Add(
                        new GetLastCandleItemResponse
                        {
                            Open = candle.Open,
                            Close = candle.Close,
                            Low = candle.Low,
                            High = candle.High,
                            Date = candle.Date,
                            Volume = candle.Volume
                        });
            }

            return response;
        }

        /// <inheritdoc />
        public async Task LoadCandlesAsync()
        {
            var instruments = await instrumentRepository.GetActiveInstrumentsAsync();

            if (instruments is null)
                return;

            foreach (var instrument in instruments)
            {
                var lastCandle = await candleRepository.GetLastCandleAsync(instrument.Ticker);

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
