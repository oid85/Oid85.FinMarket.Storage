using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities;

public class BondCouponEntity : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор инструмента
    /// </summary>
    [Column("instrument_id")]
    public Guid InstrumentId { get; set; }
    
    /// <summary>
    /// Тикер
    /// </summary>
    [Column("ticker"), MaxLength(20)]
    public string Ticker { get; set; } = string.Empty;
    
    /// <summary>
    /// Дата выплаты купона
    /// </summary>
    [Column("coupon_date", TypeName = "date")]
    public DateOnly CouponDate { get; set; } = DateOnly.MinValue;
    
    /// <summary>
    /// Номер купона
    /// </summary>
    [Column("coupon_number")]
    public long CouponNumber { get; set; }
    
    /// <summary>
    /// Купонный период в днях
    /// </summary>
    [Column("coupon_period")]
    public int CouponPeriod { get; set; }

    /// <summary>
    /// Начало купонного периода
    /// </summary>
    [Column("coupon_start_date", TypeName = "date")]
    public DateOnly CouponStartDate { get; set; } = DateOnly.MinValue;
    
    /// <summary>
    /// Окончание купонного периода
    /// </summary>
    [Column("coupon_end_date", TypeName = "date")]
    public DateOnly CouponEndDate { get; set; } = DateOnly.MinValue;
    
    /// <summary>
    /// Выплата на одну облигацию
    /// </summary>
    [Column("pay_one_bond")]
    public double PayOneBond { get; set; }
}