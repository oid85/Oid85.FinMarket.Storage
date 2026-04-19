using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    public interface IEmitentRepository
    {
        Task CreateOrUpdateEmitentAsync(Emitent emitent);
        Task<List<Emitent>> GetEmitentsAsync();
    }
}
