using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters
{
    public class InvestApiClientAdapter(
        GetInstrumentsHelper getInstrumentsHelper) 
        : IInvestApiClientAdapter
    {
        public async Task<List<Instrument>> GetInstrumentsAsync() => 
            [
                ..await getInstrumentsHelper.GetSharesAsync(),
                ..await getInstrumentsHelper.GetFuturesAsync(),
                ..await getInstrumentsHelper.GetBondsAsync(),
                ..await getInstrumentsHelper.GetIndexesAsync()
            ];
    }
}
