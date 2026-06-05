using Lemon_Test.Core.MemberData;
using Lemon_Test.Core.Tests.MemberData.TestData;

namespace Lemon_Test.Core.Tests.MemberData;

public class DiscountCalculatorTests
{
    [Theory]
    [ClassData(typeof(DiscountData))]
    public void CalculateDiscount_ShouldApplyCorrectRates(
        decimal orderAmount, 
        string customerType, 
        decimal expectedDiscount,
        string scenario)
    {
        // Arrange
        var calculator = new DiscountCalculator();

        // Act
        var result = calculator.CalculateDiscount(orderAmount, customerType);

        // Assert
        Assert.Equal(expectedDiscount, result, precision: 2);
    }
}