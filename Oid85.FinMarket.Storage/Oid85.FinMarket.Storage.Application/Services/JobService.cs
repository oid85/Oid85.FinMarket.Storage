using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class JobService(
        IInstrumentService instrumentService) : IJobService
    {
        public async Task<LoadInstrumentResponse> LoadInstrumentsAsync()
        {
            await instrumentService.LoadInstrumentsAsync();
            return new();
        }
    }
}
