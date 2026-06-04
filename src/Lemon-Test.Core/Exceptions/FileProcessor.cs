namespace Lemon_Test.Core.Exceptions;

public class FileNotFoundException : Exception
{
    public string FileName { get; }

    public FileNotFoundException(string fileName)
        : base($"File '{fileName}' was not found")
    {
        FileName = fileName;
    }
}

public class ProcessingException : Exception
{
    public string FileName { get; }
    public int LineNumber { get; }

    public ProcessingException(string fileName, int lineNumber, string message, Exception? innerException = null)
        : base($"Error processing file '{fileName}' at line {lineNumber}: {message}", innerException)
    {
        FileName = fileName;
        LineNumber = lineNumber;
    }
}

public class FileProcessor
{
    public string ProcessFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty", nameof(fileName));

        if (!fileName.EndsWith(".txt"))
            throw new ArgumentException("Only .txt files are supported", nameof(fileName));

        // Simulate file not found
        if (fileName.Contains("missing"))
            throw new FileNotFoundException(fileName);

        // Simulate processing error
        if (fileName.Contains("corrupt"))
        {
            var innerException = new InvalidDataException("File contains invalid data");
            throw new ProcessingException(fileName, 42, "Data corruption detected", innerException);
        }

        return $"Processed: {fileName}";
    }

    public async Task<string> ProcessFileAsync(string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty", nameof(fileName));

        // Simulate async processing
        await Task.Delay(100, cancellationToken);

        if (fileName.Contains("timeout"))
            throw new TimeoutException($"Processing of '{fileName}' timed out");

        if (fileName.Contains("network"))
            throw new HttpRequestException($"Network error while processing '{fileName}'");

        return $"Async processed: {fileName}";
    }

    public void ValidateAge(int age)
    {
        if (age < 0)
            throw new ArgumentOutOfRangeException(nameof(age), age, "Age cannot be negative");

        if (age > 150)
            throw new ArgumentOutOfRangeException(nameof(age), age, "Age cannot exceed 150 years");
    }
}
