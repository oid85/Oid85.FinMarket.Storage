using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// ВВП
    /// </summary>
    public class VvpEntity : BaseEntity
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        /// <summary>
        /// Изменение
        /// </summary>
        public double? Delta { get; set; }
    }
}
