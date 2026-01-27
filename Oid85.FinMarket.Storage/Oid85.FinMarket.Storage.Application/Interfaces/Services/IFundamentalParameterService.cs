using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис фундаментальных параметров
    /// </summary>
    public interface IFundamentalParameterService
    {
        /// <summary>
        /// Создание/изменение фундаментального параметра
        /// </summary>
        Task<CreateOrUpdateFundamentalParameterResponse> CreateOrUpdateFundamentalParameterAsync(CreateOrUpdateFundamentalParameterRequest request);

        /// <summary>
        /// Получить список фундаментальных параметров
        /// </summary>
        Task<GetFundamentalParameterListResponse> GetFundamentalParameterListAsync(GetFundamentalParameterListRequest request);
    }
}
