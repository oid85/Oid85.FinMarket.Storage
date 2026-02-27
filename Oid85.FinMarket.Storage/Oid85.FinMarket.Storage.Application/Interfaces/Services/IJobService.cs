namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы с запланированными задачами
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// Загрузить купоны
        /// </summary>
        Task LoadBondCouponsAsync();

        /// <summary>
        /// Загрузить свечи
        /// </summary>
        Task LoadCandlesAsync();

        /// <summary>
        /// Загрузить инструменты
        /// </summary>
        Task LoadInstrumentsAsync();
    }
}
