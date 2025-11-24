using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    /// <inheritdoc/>
    public class InstrumentService()
        : IInstrumentService
    {
        /// <inheritdoc/>
        public Task<GetInstrumentListResponse> GetInstrumentListAsync(GetInstrumentListRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
