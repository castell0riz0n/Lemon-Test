namespace Lemon_Test.Core.BasicOutput;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public decimal Divide(decimal dividend, decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot divide by zero");
        
        return dividend / divisor;
    }

    public List<int> ProcessNumbers(IEnumerable<int> numbers)
    {
        var result = new List<int>();
        
        foreach (var number in numbers)
        {
            // Simulate some processing
            var processed = number * 2;
            result.Add(processed);
        }
        
        return result;
    }
}
