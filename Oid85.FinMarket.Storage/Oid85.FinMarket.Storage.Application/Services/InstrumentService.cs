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
        IInvestApiClientAdapter investApiClientAdapter)
        : IInstrumentService
    {
        /// <inheritdoc/>
        public async Task<GetInstrumentListResponse?> GetInstrumentListAsync(GetInstrumentListRequest request)
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync() ?? [])
                .Where(x => 
                    x.Type == KnownInstrumentTypes.Share ||
                    x.Type == KnownInstrumentTypes.Index ||
                    x.Type == KnownInstrumentTypes.Future ||
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
                    Sector = x.Sector,
                    Type = x.Type,
                    MaturityDate = x.MaturityDate,
                    CouponQuantityPerYear = x.CouponQuantityPerYear,
                    Nkd = x.Nkd,
                    Nominal = x.Nominal,
                    LastPrice = x.LastPrice,
                    Currency = x.Currency
                })
                .ToList()
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task LoadInstrumentsAsync()
        {
            var instruments = await investApiClientAdapter.GetInstrumentsAsync();

            foreach (var instrument in instruments)
                await instrumentRepository.AddAsync(instrument);

            await LoadLastPricesAsync(instruments);
        }

        private async Task LoadLastPricesAsync(List<Instrument> instruments)
        {
            var instrumentIds = instruments.Select(x => x.InstrumentId).ToList();
            var prices = await investApiClientAdapter.GetLastPricesAsync(instrumentIds);

            for (var i = 0; i < prices.Count; i++)
            {
                instruments[i].LastPrice = instruments[i].Type == KnownInstrumentTypes.Bond ? prices[i] * 10.0 : prices[i];
                await instrumentRepository.AddAsync(instruments[i]);
            }
        }
    }
}
