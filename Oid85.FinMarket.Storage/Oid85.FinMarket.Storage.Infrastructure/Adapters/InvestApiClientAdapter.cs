using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters
{
    public class InvestApiClientAdapter(
        GetInstrumentsHelper getInstrumentsHelper,
        GetCandlesHelper getCandlesHelper) 
        : IInvestApiClientAdapter
    {
        public Task<List<Candle>> GetCandleAsync(Guid instrumentId, DateOnly from, DateOnly to) =>
            getCandlesHelper.GetCandlesAsync(instrumentId, from, to);

        public async Task<List<Instrument>> GetInstrumentsAsync() => 
            [
                ..await getInstrumentsHelper.GetSharesAsync(),
                ..await getInstrumentsHelper.GetFuturesAsync(),
                ..await getInstrumentsHelper.GetBondsAsync(),
                ..await getInstrumentsHelper.GetIndexesAsync()
            ];
    }
}
