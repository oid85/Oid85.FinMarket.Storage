using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oid85.FinMarket.Storage.Core.Models;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Repositories
{
    public interface IDividendRepository
    {
        Task AddAsync(List<DividendInfo> dividends);
        Task<List<DividendInfo>> GetDividendsAsync();
    }
}
