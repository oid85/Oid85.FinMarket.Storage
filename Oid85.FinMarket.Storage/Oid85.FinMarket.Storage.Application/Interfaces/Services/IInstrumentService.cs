using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы с инструментами
    /// </summary>
    public interface IInstrumentService
    {
        /// <summary>
        /// Получить список инструментов
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetInstrumentListResponse> GetInstrumentListAsync(GetInstrumentListRequest request);

        /// <summary>
        /// Загрузить инструменты
        /// </summary>
        Task LoadInstrumentsAsync();
    }
}
