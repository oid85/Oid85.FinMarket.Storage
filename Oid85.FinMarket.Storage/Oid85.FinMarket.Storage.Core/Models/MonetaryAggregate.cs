using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// Денежный агрегат
    /// </summary>
    public class MonetaryAggregate : BaseModel
    {
        /// <summary>
        /// Дата
        /// </summary>
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
