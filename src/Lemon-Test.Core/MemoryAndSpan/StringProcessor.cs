namespace Lemon_Test.Core.MemoryAndSpan;

public class StringProcessor
{
    public ReadOnlySpan<char> GetFirstWord(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
            return ReadOnlySpan<char>.Empty;

        int spaceIndex = text.IndexOf(' ');
        return spaceIndex == -1 ? text : text.Slice(0, spaceIndex);
    }

    public bool ContainsExclamation(ReadOnlySpan<char> text)
    {
        return text.Contains('!');
    }

    public void ReverseInPlace(Span<char> text)
    {
        if (text.Length <= 1)
            return;

        for (int i = 0; i < text.Length / 2; i++)
        {
            (text[i], text[^(i + 1)]) = (text[^(i + 1)], text[i]);
        }
    }

    public List<ReadOnlyMemory<char>> SplitIntoLines(ReadOnlyMemory<char> text)
    {
        var lines = new List<ReadOnlyMemory<char>>();
        var remaining = text;

        while (!remaining.IsEmpty)
        {
            var span = remaining.Span;
            int newlineIndex = span.IndexOfAny('\r', '\n');

            if (newlineIndex == -1)
            {
                // No more newlines, add the rest
                lines.Add(remaining);
                break;
            }

            // Add the line (without newline)
            if (newlineIndex > 0)
            {
                lines.Add(remaining.Slice(0, newlineIndex));
            }

            // Skip the newline character(s)
            int skipCount = 1;
            if (newlineIndex + 1 < span.Length && 
                span[newlineIndex] == '\r' && 
                span[newlineIndex + 1] == '\n')
            {
                skipCount = 2; // Skip both \r\n
            }

            remaining = remaining.Slice(newlineIndex + skipCount);
        }

        return lines;
    }
}

public class DataCompressor
{
    public Memory<byte> Compress(ReadOnlyMemory<byte> data)
    {
        // Simple run-length encoding for demo purposes
        var compressed = new List<byte>();
        var span = data.Span;

        if (span.IsEmpty)
            return Memory<byte>.Empty;

        byte currentByte = span[0];
        int count = 1;

        for (int i = 1; i < span.Length; i++)
        {
            if (span[i] == currentByte && count < 255)
            {
                count++;
            }
            else
            {
                compressed.Add((byte)count);
                compressed.Add(currentByte);
                currentByte = span[i];
                count = 1;
            }
        }

        // Add the last run
        compressed.Add((byte)count);
        compressed.Add(currentByte);

        return new Memory<byte>(compressed.ToArray());
    }
}
