using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    /// <summary>
    /// Репозиторий ВВП
    /// </summary>
    public interface IVvpRepository
    {
        Task<List<Vvp>?> GetVvpsAsync();
    }
}
