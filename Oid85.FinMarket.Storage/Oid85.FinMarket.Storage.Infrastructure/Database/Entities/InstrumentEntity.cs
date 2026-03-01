using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class InstrumentEntity : BaseEntity
    {
        /// <summary>
        /// Идентификатор инструмента
        /// </summary>
        public Guid InstrumentId { get; set; }

        /// <summary>
        /// Тикер
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сектор
        /// </summary>
        public string Sector { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дата погашения
        /// </summary>
        [Column(TypeName = "date")]
        public DateOnly? MaturityDate { get; set; } = null;

        /// <summary>
        /// Кол-во купонов в год
        /// </summary>
        public int? CouponQuantityPerYear { get; set; }

        /// <summary>
        /// НКД
        /// </summary>
        public double? Nkd { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? LastPrice { get; set; }
    }
}
