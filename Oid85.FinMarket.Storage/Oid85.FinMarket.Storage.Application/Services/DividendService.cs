using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Common.KnownConstants;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class DividendService(
        IInstrumentRepository instrumentRepository,
        IInvestApiClientAdapter investApiClientAdapter,
        IDividendRepository dividendRepository)
        : IDividendService
    {
        public async Task<GetDividendListResponse> GetDividendListAsync(GetDividendListRequest request)
        {
            var dividends = await dividendRepository.GetDividendsAsync();

            if (dividends is null)
                return new GetDividendListResponse { Dividends = [] };

            return new GetDividendListResponse
            {
                Dividends = dividends
                .Select(x =>
                new GetDividendListItemResponse
                {
                    Ticker = x.Ticker,
                    Date = x.RecordDate,
                    Value = x.Dividend
                })
                .ToList()
            };
        }

        public async Task LoadDividendsAsync()
        {
            var instruments = (await instrumentRepository.GetActiveInstrumentsAsync())?
                .Where(x => x.Type == KnownInstrumentTypes.Share)                
                .ToList();

            if (instruments is null)
                return;

            var dividends = await investApiClientAdapter.GetDividendInfosAsync(instruments);

            await dividendRepository.AddAsync(dividends);
        }
    }
}
