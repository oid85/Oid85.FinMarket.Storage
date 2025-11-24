using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.Storage.Application.Interfaces.Repositories;
using Oid85.FinMarket.Storage.Infrastructure.Database;

namespace Oid85.FinMarket.Storage.Infrastructure.Database.Repositories
{
    /// <inheritdoc/>
    public class InstrumentRepository(
        IDbContextFactory<FinMarketContext> contextFactory) 
        : IInstrumentRepository
    {

    }
}
