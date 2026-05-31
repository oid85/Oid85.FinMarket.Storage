using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// ВВП
    /// </summary>
    public class Vvp : BaseModel
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Изменение
        /// </summary>
        public double? Delta { get; set; }
    }
}
