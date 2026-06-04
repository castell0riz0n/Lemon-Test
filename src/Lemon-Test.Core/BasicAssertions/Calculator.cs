namespace Lemon_Test.Core.BasicAssertions;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        
        return a / b;
    }

    public double CalculateAverage(double[] values)
    {
        if (values == null || values.Length == 0)
            throw new ArgumentException("Values cannot be null or empty");
        
        return values.Sum() / values.Length;
    }

    public bool IsPositive(int number)
    {
        return number > 0;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}
