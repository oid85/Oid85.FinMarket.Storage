using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models;

public class DividendInfo : BaseModel
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
    public DateOnly RecordDate { get; set; } = DateOnly.MinValue;

    /// <summary>
    /// Дата объявления дивидендов
    /// </summary>
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