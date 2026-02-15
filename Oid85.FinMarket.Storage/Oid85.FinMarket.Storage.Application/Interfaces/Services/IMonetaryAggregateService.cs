using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис денежных агрегатов
    /// </summary>
    public interface IMonetaryAggregateService
    {
        Task<CreateOrUpdateMonetaryAggregateResponse> CreateOrUpdateMonetaryAggregateAsync(CreateOrUpdateMonetaryAggregateRequest request);
        Task<GetMonetaryAggregateListResponse> GetMonetaryAggregateListAsync(GetMonetaryAggregateListRequest request);
    }
}
