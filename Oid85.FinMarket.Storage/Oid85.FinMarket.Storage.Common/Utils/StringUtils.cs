using System.Globalization;

namespace Oid85.FinMarket.Storage.Common.Utils;

public static class StringUtils
{  
    public static string Base64Encode(string text) => 
        Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(text));

    public static string Base64Decode(string base64) => 
        System.Text.Encoding.UTF8.GetString(
            Convert.FromBase64String(base64));

    public static double ToDouble(string? input)
    {
        if (input is null) return 0.0;

        string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        input = input.Trim();
        input = input.Replace(" ", "");
        input = input.Replace(",", separator);
        input = input.Replace(".", separator);

        var result = Convert.ToDouble(input);

        return result;
    }
}