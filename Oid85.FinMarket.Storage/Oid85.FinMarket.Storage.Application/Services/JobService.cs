using Oid85.FinMarket.Storage.Application.Interfaces.Services;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Services
{
    public class JobService(
        IInstrumentService instrumentService,
        ICandleService candleService) 
        : IJobService
    {
        public async Task<LoadCandlesResponse> LoadCandlesAsync()
        {
            await candleService.LoadCandlesAsync();
            return new();
        }

        public async Task<LoadInstrumentsResponse> LoadInstrumentsAsync()
        {
            await instrumentService.LoadInstrumentsAsync();
            return new();
        }
    }
}
