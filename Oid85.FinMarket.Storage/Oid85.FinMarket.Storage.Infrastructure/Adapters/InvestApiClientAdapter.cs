using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Infrastructure.Adapters
{
    public class InvestApiClientAdapter(
        GetInstrumentsHelper getInstrumentsHelper) 
        : IInvestApiClientAdapter
    {
        public async Task<List<Instrument>> GetInstrumentsAsync()
        {
            var shares = await getInstrumentsHelper.GetSharesAsync();

            return [.. shares];
        }
    }
}
