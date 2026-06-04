namespace Lemon_Test.Core.MemoryAndSpan;

public class BufferProcessor
{
    public void ProcessBuffer(Memory<byte> buffer)
    {
        var span = buffer.Span;
        for (int i = 0; i < span.Length; i++)
        {
            span[i] *= 2; // Double each value
        }
    }

    public ReadOnlySpan<char> ExtractSubstring(ReadOnlyMemory<char> text, int start, int length)
    {
        if (start < 0 || start >= text.Length)
            throw new ArgumentOutOfRangeException(nameof(start));

        if (length < 0 || start + length > text.Length)
            throw new ArgumentOutOfRangeException(nameof(length));

        return text.Slice(start, length).Span;
    }

    public int CountWords(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
            return 0;

        int wordCount = 0;
        bool inWord = false;

        foreach (char c in text)
        {
            if (char.IsWhiteSpace(c))
            {
                inWord = false;
            }
            else if (!inWord)
            {
                inWord = true;
                wordCount++;
            }
        }

        return wordCount;
    }

    public bool ContainsPattern(ReadOnlySpan<byte> data, ReadOnlySpan<byte> pattern)
    {
        if (pattern.IsEmpty)
            return true;

        if (data.Length < pattern.Length)
            return false;

        for (int i = 0; i <= data.Length - pattern.Length; i++)
        {
            if (data.Slice(i, pattern.Length).SequenceEqual(pattern))
                return true;
        }

        return false;
    }
}

public class MemoryWindowProcessor
{
    public List<Memory<T>> CreateWindows<T>(Memory<T> source, int windowSize)
    {
        if (windowSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(windowSize));

        var windows = new List<Memory<T>>();
        
        for (int i = 0; i + windowSize <= source.Length; i += windowSize)
        {
            windows.Add(source.Slice(i, windowSize));
        }

        return windows;
    }

    public List<ReadOnlyMemory<T>> CreateOverlappingWindows<T>(ReadOnlyMemory<T> source, int windowSize, int step)
    {
        if (windowSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(windowSize));
        
        if (step <= 0)
            throw new ArgumentOutOfRangeException(nameof(step));

        var windows = new List<ReadOnlyMemory<T>>();
        
        for (int i = 0; i + windowSize <= source.Length; i += step)
        {
            windows.Add(source.Slice(i, windowSize));
        }

        return windows;
    }
}
