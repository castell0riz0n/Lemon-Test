using System.Globalization;

namespace Lemon_Test.Core.StringAndNumeric;

public class PriceCalculator
{
    public decimal CalculateTotal(decimal[] prices)
    {
        if (prices == null || prices.Length == 0)
            return 0;

        return prices.Sum();
    }

    public decimal ApplyDiscount(decimal originalPrice, decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100");

        return originalPrice * (1 - discountPercentage / 100);
    }

    public string FormatCurrency(decimal amount, string currencyCode = "USD")
    {
        var culture = currencyCode switch
        {
            "USD" => new CultureInfo("en-US"),
            "EUR" => new CultureInfo("de-DE"),
            "GBP" => new CultureInfo("en-GB"),
            _ => CultureInfo.CurrentCulture
        };

        return amount.ToString("C", culture);
    }

    public bool IsValidPrice(decimal price)
    {
        return price >= 0 && price <= 999999.99m;
    }

    public int GetRandomScore()
    {
        var random = new Random();
        return random.Next(0, 101); // 0 to 100 inclusive
    }
}
