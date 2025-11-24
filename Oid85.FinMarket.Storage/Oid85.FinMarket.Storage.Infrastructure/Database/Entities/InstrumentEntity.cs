using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class InstrumentEntity : BaseEntity
    {
        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; set; }
    }
}
