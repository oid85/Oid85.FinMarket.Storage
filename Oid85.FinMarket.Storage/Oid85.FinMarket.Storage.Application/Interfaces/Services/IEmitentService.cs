using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    public interface IEmitentService
    {
        Task<CreateOrUpdateEmitentResponse> CreateOrUpdateEmitentAsync(CreateOrUpdateEmitentRequest request);
        Task<GetEmitentListResponse> GetEmitentListAsync(GetEmitentListRequest request);
    }
}
