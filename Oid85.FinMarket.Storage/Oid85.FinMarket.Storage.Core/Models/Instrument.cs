using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class Instrument : BaseModel
    {
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
