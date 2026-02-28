using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models;

public class BondCoupon : BaseModel
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
    /// Дата выплаты купона
    /// </summary>
    public DateOnly CouponDate { get; set; } = DateOnly.MinValue;
    
    /// <summary>
    /// Номер купона
    /// </summary>
    public long CouponNumber { get; set; }
    
    /// <summary>
    /// Купонный период в днях
    /// </summary>
    public int CouponPeriod { get; set; }
    
    /// <summary>
    /// Выплата на одну облигацию
    /// </summary>
    public double PayOneBond { get; set; }
}