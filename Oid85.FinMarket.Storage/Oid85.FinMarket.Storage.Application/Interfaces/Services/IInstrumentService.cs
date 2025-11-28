namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы с инструментами
    /// </summary>
    public interface IInstrumentService
    {
        /// <summary>
        /// Загрузить инструменты
        /// </summary>
        Task LoadInstrumentsAsync();
    }
}
