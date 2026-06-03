namespace Lemon_Test.Core.FactsVsTheories;

public class Calculator
{
    
    public int Add(int first, int second)
    {
        return first + second;
    }

    public double Divide(int dividend, int zeroDivisor)
    {
        return dividend / zeroDivisor;
    }
    
    public decimal Divide(decimal a, decimal b)
    {
        if (b == 0)
            throw new DivideByZeroException();
        
        return a / b;
    }

    public int Subtract(int minuend, int subtrahend)
    {
        return minuend - subtrahend;
    }
}
