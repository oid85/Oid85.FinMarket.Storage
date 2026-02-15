using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Изменение индекса потребительских цен
    /// </summary>
    public class ConsumerPriceIndexChangeEntity : BaseEntity
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public double? Value { get; set; }
    }
}
