namespace Lemon_Test.Core.Common;

/// <summary>
/// Simple calculator for basic lifecycle demos
/// </summary>
public class Calculator
{
    public int Add(int a, int b) => a + b;
    public int Subtract(int a, int b) => a - b;
    public double Sum(IEnumerable<double> values) => values.Sum();
}
