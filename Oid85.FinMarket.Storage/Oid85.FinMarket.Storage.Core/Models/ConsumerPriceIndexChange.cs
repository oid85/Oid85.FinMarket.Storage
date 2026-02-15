using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// Изменение индекса потребительских цен
    /// </summary>
    public class ConsumerPriceIndexChange : BaseModel
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
