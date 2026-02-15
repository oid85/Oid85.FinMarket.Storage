using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// Ключевая ставка
    /// </summary>
    public class KeyRate : BaseModel
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double? Value { get; set; }
    }
}
