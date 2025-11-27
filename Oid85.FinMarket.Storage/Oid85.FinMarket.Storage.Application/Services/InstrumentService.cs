using Oid85.FinMarket.Storage.Application.Interfaces.Adapters;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Application.Interfaces.Services;
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
        public Task<GetInstrumentListResponse> GetInstrumentListAsync(GetInstrumentListRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task LoadInstrumentsAsync()
        {
            var instruments = await investApiClientAdapter.GetInstrumentsAsync();

            foreach (var instrument in instruments)
                await instrumentRepository.AddAsync(instrument);      
        }
    }
}
