using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oid85.FinMarket.Storage.Core.Requests;
using Oid85.FinMarket.Storage.Core.Responses;

namespace Oid85.FinMarket.Storage.Application.Interfaces.Services
{
    public interface IDividendService
    {
        Task<GetDividendListResponse> GetDividendListAsync(GetDividendListRequest request);
        Task LoadDividendsAsync();
    }
}
