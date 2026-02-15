using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис ключевой ставки
    /// </summary>
    public interface IKeyRateService
    {
        Task<CreateOrUpdateKeyRateResponse> CreateOrUpdateKeyRateAsync(CreateOrUpdateKeyRateRequest request);
        Task<GetKeyRateListResponse> GetKeyRateListAsync(GetKeyRateListRequest request);
    }
}
