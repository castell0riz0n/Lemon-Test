namespace Lemon_Test.Core.InlineData;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}
