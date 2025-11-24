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
    }
}
