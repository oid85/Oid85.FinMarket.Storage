using Oid85.FinMarket.Storage.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Entities
{
    /// <summary>
    /// Эмитент
    /// </summary>
    public class EmitentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Rating { get; set; }
        public string? KeyWord { get; set; }
    }
}
