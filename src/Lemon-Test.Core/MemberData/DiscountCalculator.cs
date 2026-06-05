namespace Lemon_Test.Core.MemberData;

public class DiscountCalculator
{
    public decimal CalculateDiscount(decimal orderAmount, string customerType)
    {
        if (orderAmount < 50)
            return 0m;

        return customerType.ToUpper() switch
        {
            "VIP" => orderAmount >= 500 ? 0.15m : 0.10m,
            "STANDARD" => orderAmount >= 100 ? 0.05m : 0m,
            _ => 0m
        };
    }
}
