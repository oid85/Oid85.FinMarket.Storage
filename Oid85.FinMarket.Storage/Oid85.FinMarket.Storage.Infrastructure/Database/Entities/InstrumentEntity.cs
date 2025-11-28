using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class InstrumentEntity : BaseEntity
    {
        /// <summary>
        /// Идентификатор инструмента
        /// </summary>
        public Guid InstrumentId { get; set; }

        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }
    }
}
