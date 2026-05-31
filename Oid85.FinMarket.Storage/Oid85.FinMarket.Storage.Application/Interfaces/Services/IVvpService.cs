using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис ВВП
    /// </summary>
    public interface IVvpService
    {
        Task<GetVvpListResponse> GetVvpListAsync(GetVvpListRequest request);
    }
}
