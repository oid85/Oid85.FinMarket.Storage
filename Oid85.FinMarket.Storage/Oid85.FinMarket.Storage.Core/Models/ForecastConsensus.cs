using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models;

/// <summary>
/// Консенсус-прогноз
/// </summary>
public class ForecastConsensus : BaseModel
{
    /// <summary>
    /// Тикер
    /// </summary>
    public string Ticker { get; set; } = string.Empty;

    /// <summary>
    /// Уникальный идентификатор инструмента
    /// </summary>
    public Guid InstrumentId { get; set; }
    
    /// <summary>
    /// Прогноз строкой
    /// </summary>
    public string RecommendationString { get; set; } = string.Empty;
    
    /// <summary>
    /// Прогноз числом
    /// </summary>
    public int RecommendationNumber { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; } = string.Empty;
    
    /// <summary>
    /// Текущая цена
    /// </summary>
    public double CurrentPrice { get; set; }
    
    /// <summary>
    /// Прогнозируемая цена
    /// </summary>
    public double ConsensusPrice { get; set; }
    
    /// <summary>
    /// Минимальная цена прогноза
    /// </summary>
    public double MinTarget { get; set; }
    
    /// <summary>
    /// Максимальная цена прогноза
    /// </summary>
    public double MaxTarget { get; set; }
    
    /// <summary>
    /// Изменение цены
    /// </summary>
    public double PriceChange { get; set; }
    
    /// <summary>
    /// Относительное изменение цены
    /// </summary>
    public double PriceChangeRel { get; set; }
}