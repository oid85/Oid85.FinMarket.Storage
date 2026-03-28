using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

public class DividendInfoEntity : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор инструмента
    /// </summary>
    public Guid InstrumentId { get; set; }
    
    /// <summary>
    /// Тикер
    /// </summary>
    public string Ticker { get; set; } = string.Empty;

    /// <summary>
    /// Дата фиксации реестра
    /// </summary>
    [Column(TypeName = "date")]
    public DateOnly RecordDate { get; set; } = DateOnly.MinValue;

    /// <summary>
    /// Дата объявления дивидендов
    /// </summary>
    [Column(TypeName = "date")]
    public DateOnly DeclaredDate { get; set; } = DateOnly.MinValue;

    /// <summary>
    /// Выплата, руб
    /// </summary>
    public double Dividend { get; set; }

    /// <summary>
    /// Доходность, %
    /// </summary>
    public double DividendPrc { get; set; }
}