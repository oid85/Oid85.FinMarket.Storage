
namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы со свечами
    /// </summary>
    public interface ICandleService
    {
        /// <summary>
        /// Загрузить свечи
        /// </summary>
        Task LoadCandlesAsync();
    }
}
