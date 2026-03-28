using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    public interface IForecastService
    {
        Task<GetForecastListResponse> GetForecastListAsync(GetForecastListRequest request);
        Task LoadForecastConsensusesAsync();
    }
}
