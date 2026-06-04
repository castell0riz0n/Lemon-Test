using System.Text.RegularExpressions;

namespace Lemon_Test.Core.StringAndNumeric;

public class OrderNumberGenerator
{
    private static int _counter = 1;

    public string GenerateOrderNumber()
    {
        var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        var sequencePart = _counter.ToString("D4");
        _counter++;

        return $"ORD-{datePart}-{sequencePart}";
    }

    public bool IsValidOrderNumber(string orderNumber)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            return false;

        var pattern = @"^ORD-\d{8}-\d{4}$";
        return Regex.IsMatch(orderNumber, pattern);
    }
}

public class TextProcessor
{
    public string ProcessText(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var processed = input.Trim().ToUpperInvariant();
        return $"PROCESSED: {processed}";
    }

    public string ExtractDomain(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return string.Empty;

        var parts = email.Split('@');
        return parts.Length == 2 ? parts[1] : string.Empty;
    }

    public bool ContainsKeyword(string text, string keyword)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(keyword))
            return false;

        return text.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}
