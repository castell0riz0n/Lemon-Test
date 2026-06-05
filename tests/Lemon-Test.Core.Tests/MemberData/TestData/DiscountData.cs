using System.Collections;

namespace Lemon_Test.Core.Tests.MemberData.TestData;

public class DiscountData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        // No discount scenarios
        yield return new object[] { 25.00m, "STANDARD", 0.00m, "Below minimum order" };
        yield return new object[] { 75.00m, "STANDARD", 0.00m, "Standard customer below discount threshold" };

        // Standard customer discounts
        yield return new object[] { 100.00m, "STANDARD", 0.05m, "Standard customer - basic discount" };
        yield return new object[] { 200.00m, "STANDARD", 0.05m, "Standard customer - higher amount" };

        // VIP customer discounts
        yield return new object[] { 100.00m, "VIP", 0.10m, "VIP customer - basic discount" };
        yield return new object[] { 500.00m, "VIP", 0.15m, "VIP customer - premium discount" };

        // Unknown customer type
        yield return new object[] { 100.00m, "UNKNOWN", 0.00m, "Unknown customer type" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}