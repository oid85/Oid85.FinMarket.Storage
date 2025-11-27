using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    public interface IJobService
    {
        Task<LoadInstrumentResponse> LoadInstrumentsAsync();
    }
}
