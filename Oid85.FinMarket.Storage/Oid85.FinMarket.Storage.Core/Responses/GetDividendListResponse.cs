using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oid85.FinMarket.Storage.Core.Responses
{
    public class GetDividendListResponse
    {
        public List<GetDividendListItemResponse> Dividends { get; set; } = [];
    }

    public class GetDividendListItemResponse
    {
        public string Ticker { get; set; }
        public DateOnly Date { get; set; }
        public double Value { get; set; }
    }
}
