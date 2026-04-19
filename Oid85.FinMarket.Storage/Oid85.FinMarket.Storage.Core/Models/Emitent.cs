using Oid85.FinMarket.Storage.Core.Models.Base;

namespace Oid85.FinMarket.Storage.Core.Models
{
    /// <summary>
    /// Эмитент
    /// </summary>
    public class Emitent : BaseModel
    {
        public string Name { get; set; }
        public string? Rating { get; set; }
        public string? KeyWord { get; set; }
    }
}
