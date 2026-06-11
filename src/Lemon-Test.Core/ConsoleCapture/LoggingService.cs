namespace Lemon_Test.Core.ConsoleCapture;

public class LoggingService
{
    public string ProcessWithLogging(string input)
    {
        Console.WriteLine("Starting processing...");
        Console.WriteLine($"Input received: {input}");
        
        // Simulate some work
        var result = $"Processed: {input.ToUpper()}";
        
        Console.WriteLine($"Processing completed: {result}");
        Console.WriteLine("Operation finished successfully");
        
        return result;
    }

    public void ProcessBatch(IEnumerable<string> items)
    {
        Console.WriteLine($"Starting batch processing of {items.Count()} items");
        
        var processed = 0;
        foreach (var item in items)
        {
            Console.WriteLine($"Processing item {processed + 1}: {item}");
            processed++;
        }
        
        Console.WriteLine($"Batch processing completed. {processed} items processed.");
    }
}
