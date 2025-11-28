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
            try
            {
                await candleService.LoadCandlesAsync();
            }

            catch
            {
                return new LoadCandlesResponse { Result = false };
            }

            return new LoadCandlesResponse { Result = true };
        }

        public async Task<LoadInstrumentsResponse> LoadInstrumentsAsync()
        {
            try
            {
                await instrumentService.LoadInstrumentsAsync();
            }

            catch
            {
                return new LoadInstrumentsResponse { Result = false };
            }

            return new LoadInstrumentsResponse { Result = true };
        }
    }
}
