namespace Oid85.FinMarket.Storage.Common.Helpers;

public static class DateHelper
{
    public static List<DateOnly> GetDates(DateOnly from, DateOnly to)
    {
        var curDate = from;
        var dates = new List<DateOnly>();
        
        while (curDate <= to)
        {
            dates.Add(curDate);            
            curDate = curDate.AddDays(1);
        }

        return dates;
    }
}