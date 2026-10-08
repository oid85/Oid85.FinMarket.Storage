using System.Diagnostics.Metrics;
using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Core.Models;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc/>
    public class InstrumentService(
        IInstrumentRepository instrumentRepository,
        IEmitentRepository emitentRepository,
        IInvestApiClientAdapter investApiClientAdapter)
        : IInstrumentService
    {
        /// <inheritdoc/>
        public async Task<GetInstrumentListResponse?> GetInstrumentListAsync(GetInstrumentListRequest request)
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync() ?? [])
                .Where(x =>
                    x.Type == KnownInstrumentTypes.Share ||
                    x.Type == KnownInstrumentTypes.Etf ||
                    x.Type == KnownInstrumentTypes.Index ||
                    (x.Type == KnownInstrumentTypes.Future && x.MaturityDate >= DateOnly.FromDateTime(DateTime.Today)) ||
                    (x.Type == KnownInstrumentTypes.Bond && x.MaturityDate >= DateOnly.FromDateTime(DateTime.Today)))
                .ToList();

            if (instruments is null)
                return null;

            var response = new GetInstrumentListResponse
            {
                Instruments = instruments
                .Select(x => new GetInstrumentListItemResponse
                {
                    Ticker = x.Ticker,
                    Name = x.Name,
                    Type = x.Type,
                    MaturityDate = x.MaturityDate,
                    CouponQuantityPerYear = x.CouponQuantityPerYear,
                    Nkd = x.Nkd,
                    Nominal = x.Nominal,
                    LastPrice = x.LastPrice,
                    Currency = x.Currency,
                    Lot = x.Lot,
                    Rating = x.Rating,
                    FloatingCouponFlag = x.FloatingCouponFlag
                })
                .ToList()
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task<GetInstrumentPriceResponse> GetInstrumentPriceAsync(GetInstrumentPriceRequest request)
        {
            var instruments = (await instrumentRepository.GetInstrumentsAsync() ?? [])
                .Where(x => request.Tickers.Contains(x.Ticker))
                .ToList();

            var instrumentIds = instruments.Select(x => x.InstrumentId).ToList();
            var lastPrices = await investApiClientAdapter.GetLastPricesAsync(instrumentIds);

            var response = new GetInstrumentPriceResponse();

            for (int i = 0; i < instruments.Count; i++)
                response.Items.Add(
                    new TickerPriceItem
                    {
                        Ticker = instruments[i].Ticker,
                        Price = lastPrices[i]
                    });

            return response;
        }

        /// <inheritdoc/>
        public async Task LoadInstrumentsAsync()
        {
            var instruments = await investApiClientAdapter.GetInstrumentsAsync();

            // Добавляем инструменты в БД
            foreach (var instrument in instruments)
                await instrumentRepository.AddAsync(instrument);

            // Удаляем облигации с истекшим сроком погашения
            await instrumentRepository.DeleteOldBondsAsync();

            // Устанавливаем рейтинг облигаций
            await SetBondRatingAsync();

            // Отмечаем облигации с хорошим рейтингом и с постоянным купоном
            await SetActiveBondAsync();

            // Загружаем последние цены инструментов            
            await LoadLastPricesAsync();
        }

        private async Task LoadLastPricesAsync()
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync()) ?? [];

            var instrumentIds = instruments.Select(x => x.InstrumentId).ToList();
            
            var prices = await investApiClientAdapter.GetLastPricesAsync(instrumentIds);

            for (var i = 0; i < prices.Count; i++)
            {
                instruments[i].LastPrice = instruments[i].Type == KnownInstrumentTypes.Bond 
                    ? instruments[i].Nominal * prices[i] / 100.0 
                    : prices[i];
                
                await instrumentRepository.AddAsync(instruments[i]);
            }
        }

        private async Task SetBondRatingAsync()
        {
            var instruments = (await instrumentRepository.GetInstrumentsAsync()) ?? [];
            var bonds = instruments.Where(x => x.Type == KnownInstrumentTypes.Bond).ToList();
            var emitents = await emitentRepository.GetEmitentsAsync();

            foreach (var bond in bonds)                
                foreach (var emitent in emitents)
                {
                    var keyWords = emitent.KeyWord!.Split(';').ToList();

                    foreach (var keyWord in keyWords)
                        if (bond.Name.Contains(keyWord))
                            await instrumentRepository.SetRatingAsync(bond.Id, emitent.Rating ?? string.Empty);
                }
        }

        private async Task SetActiveBondAsync()
        {
            var instruments = (await instrumentRepository.GetInstrumentsAsync()) ?? [];
            var bonds = instruments.Where(x => x.Type == KnownInstrumentTypes.Bond).ToList();

            foreach (var bond in bonds)
                await instrumentRepository.SetActiveFlagAsync(bond.Id, false);

            List<string> goodRatings = ["AAA", "AA", "A"];

            var filteredBonds = bonds
                .Where(x => x.MaturityDate >= DateOnly.FromDateTime(DateTime.Today))
                .Where(x => x.Rating is not null)
                .Where(x => !string.IsNullOrEmpty(x.Rating))
                .Where(x => goodRatings.Contains(x.Rating ?? string.Empty))
                .Where(x => x.LastPrice is not null)
                .Where(x => x.LastPrice > 0)
                .Where(x => x.Nominal is not null)
                .Where(x => x.Nominal == 1000.0)
                .Where(x => x.Currency is not null)
                .Where(x => string.Equals(x.Currency, "rub", StringComparison.InvariantCultureIgnoreCase))                
                .Where(x => x.CouponQuantityPerYear > 0)
                .ToList();

            foreach (var bond in filteredBonds)
                await instrumentRepository.SetActiveFlagAsync(bond.Id, true);
        }
    }
}
