using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Денежный агрегат
    /// </summary>
    public class MonetaryAggregateEntity : BaseEntity
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        /// <summary>
        /// M0
        /// </summary>
        public double? M0 { get; set; }

        /// <summary>
        /// M1
        /// </summary>
        public double? M1 { get; set; }

        /// <summary>
        /// M2
        /// </summary>
        public double? M2 { get; set; }

        /// <summary>
        /// М2Х
        /// </summary>
        public double? M2X { get; set; }
    }
}
