using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    public interface IForecastRepository
    {
        Task AddAsync(List<ForecastConsensus> forecasts);
        Task<List<ForecastConsensus>> GetForecastsAsync();
    }
}
