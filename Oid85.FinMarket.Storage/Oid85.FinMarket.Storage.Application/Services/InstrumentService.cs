using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;
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
            var instruments = await instrumentRepository.GetActiveInstrumentsAsync();

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
                    LastPrice = x.LastPrice
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

            await LoadLastPricesAsync();
        }

        private async Task LoadLastPricesAsync()
        {
            var activeInstruments = (await instrumentRepository.GetActiveInstrumentsAsync()) ?? [];
            var instrumentIds = activeInstruments.Select(x => x.InstrumentId).ToList();
            var prices = await investApiClientAdapter.GetLastPricesAsync(instrumentIds);

            for (var i = 0; i < prices.Count; i++)
            {
                activeInstruments[i].LastPrice = activeInstruments[i].Type == KnownInstrumentTypes.Bond ? prices[i] * 10.0 : prices[i];
                await instrumentRepository.AddAsync(activeInstruments[i]);
            }
        }
    }
}
